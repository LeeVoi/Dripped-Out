using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
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
        
        public void AddSizesToProduct(int productId, List<int> sizeIds)
        {
            using (var con= _dbConnection.GetConnection())
            {
                con.Open();
                
                const string sql =
                    "INSERT INTO productsizes (productid, sizeid) VALUES (@productId, @sizeId)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    foreach (var sizeId in sizeIds)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@sizeId", sizeId);
                        command.ExecuteNonQuery();
                    }

                }
            }
        }

        public void RemoveSizesFromProduct(int productId, List<int> sizeIds)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "DELETE FROM productsizes WHERE productid = @productId AND sizeid = @sizeId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productId", productId);

                    foreach (var sizeId in sizeIds)
                    {
                        command.Parameters.AddWithValue("@sizeId", sizeId);
                        command.ExecuteNonQuery();
                        command.Parameters.Remove("@sizeId");
                    }
                }
            }
        }

        public List<SizeType> GetAllSizesByProductId(int productId)
        {
            var sizes = new List<SizeType>();

            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "SELECT s.* " + 
            "FROM sizetype s " +
            "JOIN productsizes ps ON s.sizeid = ps.sizeid " +
            "WHERE ps.productid = @productId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var size = new SizeType
                            {
                                SizeId = reader.GetInt32(reader.GetOrdinal("sizeid")),
                                Size = reader.GetString(reader.GetOrdinal("size")),
                            };

                            sizes.Add(size);
                        }
                    }
                }
            }

            return sizes;
        }

        public List<SizeType> GetAllSizeTypes()
        {
            var sizes = new List<SizeType>();

            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "SELECT * FROM sizetype";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var size = new SizeType
                            {
                                SizeId = reader.GetInt32(reader.GetOrdinal("sizeid")),
                                Size = reader.GetString(reader.GetOrdinal("size")),
                            };

                            sizes.Add(size);
                        }
                    }
                }
            }

            return sizes;
        }
    }
}