namespace MiniORM;

/// <summary>
/// Used for wrapping a database connection with a using statement and
/// automatically closing it when the using statement ends
/// </summary>
internal class ConnectionManager : IDisposable
{
    private readonly DatabaseConnection _connection;
    public ConnectionManager(DatabaseConnection connection)
    {
        this._connection = connection;

        this._connection.Open();
    }

    public void Dispose()
    {
        this._connection.Close();
    }
}
