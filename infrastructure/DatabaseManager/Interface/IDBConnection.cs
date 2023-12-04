using Npgsql;

namespace infrastructure.DatabaseManager.Interface
{
    public interface IDBConnection
    {
        NpgsqlConnection GetConnection();
    }
}