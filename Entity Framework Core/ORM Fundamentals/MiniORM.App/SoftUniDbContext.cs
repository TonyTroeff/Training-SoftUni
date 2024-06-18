namespace MiniORM.App;

using MiniORM.App.Entities;

public class SoftUniDbContext : DbContext
{
    public SoftUniDbContext(string connectionString) : base(connectionString)
    {
    }

    public DbSet<Department> Departments { get; } = null!;
    public DbSet<Employee> Employees { get; } = null!;
    public DbSet<EmployeeProject> EmployeesProjects { get; } = null!;
    public DbSet<Project> Projects { get; } = null!;
}