namespace MiniORM;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

public abstract class DbContext
{
    private readonly DatabaseConnection _connection;
    private readonly IDictionary<Type, PropertyInfo> _dbSetProperties;
    private readonly IDictionary<Type, DbSetDescriptor> _dbSetDescriptors;

    protected DbContext(string connectionString)
    {
        this._connection = new DatabaseConnection(connectionString);

        this._dbSetProperties = this.DiscoverDbSets();

        using (new ConnectionManager(this._connection))
            this.InitializeDbSets();

        this._dbSetDescriptors = this.DescribeDbSets();
        MapAllRelations();
    }

    internal static HashSet<Type> AllowedSqlTypes { get; } = new HashSet<Type> { typeof(string), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(decimal), typeof(bool), typeof(DateTime) };

    public void SaveChanges()
    {
        foreach (var dbSetDescriptor in this._dbSetDescriptors.Values)
        {
            var invalidEntitiesCount = dbSetDescriptor.DbSet.Count(e => !IsValid(e));
            if (invalidEntitiesCount > 0)
                throw new InvalidOperationException($"{invalidEntitiesCount} invalid entities were found in DbSet {dbSetDescriptor.Name}.");
        }

        using var connectionManager = new ConnectionManager(this._connection);
        using var transaction = this._connection.StartTransaction();

        foreach (var dbSetDescriptor in this._dbSetDescriptors.Values)
        {
            var persistMethod = typeof(DbContext).GetMethod(nameof(Persist), BindingFlags.Instance | BindingFlags.NonPublic)!.MakeGenericMethod(dbSetDescriptor.EntityType);

            try
            {
                try
                {
                    persistMethod.Invoke(this, new object?[] { dbSetDescriptor.DbSet });
                }
                catch (TargetInvocationException e) when (e.InnerException is not null)
                {
                    throw e.InnerException;
                }
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        transaction.Commit();
    }

    private void Persist<T>(DbSet<T> dbSet)
        where T : class, new()
    {
        var entityType = typeof(T);
        var tableName = GetTableName(entityType);
        var columnNames = GetColumnNames(tableName).ToArray();

        if (dbSet.ChangeTracker.Added.Count > 0)
            this._connection.InsertEntities(dbSet.ChangeTracker.Added, tableName, columnNames);

        var modifiedEntities = dbSet.ChangeTracker.GetModifiedEntities(dbSet).ToArray();
        if (modifiedEntities.Length > 0)
            this._connection.UpdateEntities(modifiedEntities, tableName, columnNames);

        if (dbSet.ChangeTracker.Removed.Count > 0)
            this._connection.DeleteEntities(dbSet.ChangeTracker.Removed, tableName, columnNames);
    }

    private static bool IsValid(object entity)
    {
        var validationContext = new ValidationContext(entity);
        return Validator.TryValidateObject(entity, validationContext, null);
    }

    private IDictionary<Type, PropertyInfo> DiscoverDbSets()
    {
        var dbSetProperties = this.GetType().GetProperties().Where(pi => pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        var result = new Dictionary<Type, PropertyInfo>();
        foreach (var property in dbSetProperties)
        {
            var entityType = property.PropertyType.GenericTypeArguments[0];
            result[entityType] = property;
        }

        return result;
    }

    private IDictionary<Type, DbSetDescriptor> DescribeDbSets()
    {
        var result = new Dictionary<Type, DbSetDescriptor>();
        foreach (var (entityType, dbSetProperty) in this._dbSetProperties)
        {
            var name = dbSetProperty.Name;

            var dbSet = dbSetProperty.GetValue(this);
            if (dbSet is not IEnumerable<object> typedDbSet)
                throw new InvalidOperationException($"DbSet {name} was not initialized correctly.");

            var descriptor = new DbSetDescriptor(entityType, name, typedDbSet);
            result[entityType] = descriptor;
        }

        return result;
    }

    private void InitializeDbSets()
    {
        foreach (var (entityType, dbSetProperty) in this._dbSetProperties)
        {
            var populateMethod = typeof(DbContext).GetMethod(nameof(PopulateDbSet), BindingFlags.Instance | BindingFlags.NonPublic)!.MakeGenericMethod(entityType);

            populateMethod.Invoke(this, new object?[] { dbSetProperty });
        }
    }

    private void MapAllRelations()
    {
        foreach (var dbSetDescriptor in this._dbSetDescriptors.Values)
        {
            var mapRelationsMethod = typeof(DbContext).GetMethod(nameof(MapRelations), BindingFlags.Instance | BindingFlags.NonPublic)!.MakeGenericMethod(dbSetDescriptor.EntityType);

            mapRelationsMethod.Invoke(this, new object?[] { dbSetDescriptor.DbSet });
        }
    }

    private void PopulateDbSet<T>(PropertyInfo dbSetProperty)
        where T : class, new()
    {
        var entities = LoadEntities<T>();
        var dbSet = new DbSet<T>(entities);
        ReflectionHelper.ReplaceBackingField(this, dbSetProperty.Name, dbSet);
    }

    private void MapRelations<T>(DbSet<T> dbSet)
        where T : class, new()
    {
        MapNavigationProperties(dbSet);

        var entityType = typeof(T);
        var collectionProperties = entityType.GetProperties().Where(pi => pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));
        foreach (var property in collectionProperties)
        {
            var relatedEntityType = property.PropertyType.GenericTypeArguments[0];
            var mapCollectionMethod = typeof(DbContext).GetMethod(nameof(MapCollection), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(entityType, relatedEntityType);

            mapCollectionMethod.Invoke(this, new object?[] { dbSet, property });
        }
    }

    private void MapNavigationProperties<T>(DbSet<T> dbSet)
        where T : class, new()
    {
        var entityType = typeof(T);
        foreach (var foreignKeyProperty in entityType.GetProperties())
        {
            var foreignKeyAttribute = foreignKeyProperty.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKeyAttribute is null) continue;

            var navigationPropertyName = foreignKeyAttribute.Name;
            var navigationProperty = entityType.GetProperty(navigationPropertyName);
            if (navigationProperty is null)
                throw new InvalidOperationException($"Navigation property with name {navigationPropertyName} was not found");

            var relatedEntityType = navigationProperty.PropertyType;
            var relatedEntityKey = relatedEntityType.GetSingleKeyProperty();
            var relatedDbSet = this._dbSetDescriptors[relatedEntityType].DbSet;
                
            foreach (var entity in dbSet.Entities)
            {
                var foreignKey = foreignKeyProperty.GetValue(entity);
                var relatedEntity = relatedDbSet
                    .Single(re => Equals(relatedEntityKey.GetValue(re), foreignKey));

                navigationProperty.SetValue(entity, relatedEntity);
            }
        }
    }

    private void MapCollection<TEntity, TRelated>(DbSet<TEntity> dbSet, PropertyInfo collectionProperty)
        where TEntity : class, new()
        where TRelated : class, new()
    {
        var entityType = typeof(TEntity);
        var relatedEntityType = typeof(TRelated);

        var primaryKeyProperty = entityType.GetSingleKeyProperty();
        var relatedDbSet = this._dbSetDescriptors[relatedEntityType].DbSet;

        var foreignKeyProperty = GetSingleForeignKeyProperty(relatedEntityType.GetProperties(), entityType);
        if (foreignKeyProperty is null) return;

        foreach (var entity in dbSet.Entities)
        {
            var primaryKey = primaryKeyProperty.GetValue(entity);

            var relatedEntities = relatedDbSet.Where(re => Equals(foreignKeyProperty.GetValue(re), primaryKey)).Cast<TRelated>().ToArray();

            collectionProperty.SetValue(entity, relatedEntities);
            // ReflectionHelper.ReplaceBackingField(entity, collectionProperty.Name, relatedEntities);
        }
    }

    private static PropertyInfo? GetSingleForeignKeyProperty(PropertyInfo[] properties, Type navigationType)
    {
        foreach (var property in properties)
        {
            var foreignKeyAttribute = property.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKeyAttribute is null) continue;

            var navigationPropertyName = foreignKeyAttribute.Name;
            var navigationProperty = property.DeclaringType!.GetProperty(navigationPropertyName);
            if (navigationProperty is null)
                throw new InvalidOperationException($"Navigation property with name {navigationPropertyName} was not found");

            if (navigationProperty.PropertyType == navigationType) return property;
        }

        return null;
    }

    private IEnumerable<T> LoadEntities<T>()
        where T : class, new()
    {
        var entityType = typeof(T);

        var tableName = this.GetTableName(entityType);
        var columnNames = this.GetColumnNames(tableName).ToArray();

        return this._connection.FetchResultSet<T>(tableName, columnNames);
    }

    private string GetTableName(Type entityType)
    {
        var tableAttribute = entityType.GetCustomAttribute<TableAttribute>();
        if (tableAttribute is not null) return tableAttribute.Name;

        var dbSetProperty = this._dbSetProperties[entityType];
        return dbSetProperty.Name;
    }

    private IEnumerable<string> GetColumnNames(string tableName)
    {
        return this._connection.FetchColumnNames(tableName);
    }

    private class DbSetDescriptor
    {
        public DbSetDescriptor(Type entityType, string name, IEnumerable<object> dbSet)
        {
            this.EntityType = entityType;
            this.Name = name;
            this.DbSet = dbSet;
        }

        public Type EntityType { get; }
        public string Name { get; }
        public IEnumerable<object> DbSet { get; }
    }
}