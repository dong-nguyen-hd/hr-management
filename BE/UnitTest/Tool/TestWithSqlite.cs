using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.Tool;

public abstract class TestWithSqlite<T> : IDisposable where T : DbContext
{
    #region Property
    private const string ConnectionString = "DataSource=:memory:";
    private readonly SqliteConnection _connection;
    protected DbContextOptions<T> Options { get; }
    #endregion

    #region Constructor
    protected TestWithSqlite()
    {
            this._connection = new SqliteConnection(ConnectionString);
            this._connection.Open();
            this.Options = new DbContextOptionsBuilder<T>().UseSqlite(_connection).Options;
        }
    #endregion

    #region Method
    public void Dispose() => _connection.Close();
    #endregion
}