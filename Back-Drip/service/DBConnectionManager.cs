namespace DefaultNamespace;

public class service
{
    private string serverName;
    private string databaseName;
    private string userName;
    private string password;

    public DatabaseConnectionHandler()
    {
        var dbConfig = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("/path/to/your/dbconfig.json"));
        serverName = dbConfig.ServerName;
        databaseName = dbConfig.DatabaseName;
        userName = dbConfig.UserName;
        password = dbConfig.Password;
    }

    public SqlConnection GetConnection()
    {
        string connectionString = $"Data Source={serverName};Initial Catalog={databaseName};User ID={userName};Password={password}";
        return new SqlConnection(connectionString);
    }
}