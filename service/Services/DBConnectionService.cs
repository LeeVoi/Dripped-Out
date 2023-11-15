using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace service.Services
{
    public class DBConnectionService
    {
        private string serverName;
        private string databaseName;
        private string userName;
        private string password;

        public DBConnectionService()
        {
            var dbConfig = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("/resources/dbconfig.json"));
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
}