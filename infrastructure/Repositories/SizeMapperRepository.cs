using infrastructure.DatabaseManager.Interface;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories
{
    public class SizeMapperRepository : ISizeMapper
    {
        private readonly IDBConnection _dbConnection;

        public SizeMapperRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public void addSizeToProduct(int ProductId, int SizeId)
        {
            using (var con= _dbConnection.GetConnection())
            {
                con.Open();
                const string sql =
                    "INSERT INTO productsizes (productid, sizeid) VALUES (@productid, @sizeid)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productid", ProductId);
                    command.Parameters.AddWithValue("@typeid", SizeId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}