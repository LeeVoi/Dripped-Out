using infrastructure.DatabaseManager.Interface;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories
{
    public class ColorMapperRepository : IColorMapper
    {
        private readonly IDBConnection _dbConnection;

        public ColorMapperRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        
        public void addColorToProduct(int ProductId, int ColorId)
        {
            using (var con= _dbConnection.GetConnection())
            {
                con.Open();
                const string sql =
                    "INSERT INTO productcolors (productid, colorid) VALUES (@productid, @colorid)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productid", ProductId);
                    command.Parameters.AddWithValue("@typeid", ColorId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}