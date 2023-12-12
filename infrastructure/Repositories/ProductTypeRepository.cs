using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories
{
    public class ProductTypeRepository : ICrud<ProductType>
    {
        private readonly IDBConnection _dbConnection;

        public ProductTypeRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Create(ProductType type)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "INSERT INTO producttype(type) VALUES (@type)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@type", type.Type);
                    command.ExecuteNonQuery();
                }
            }
        }

        public ProductType Read(int typeId)
        {
            var types = new List<ProductType>();
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "SELECT * FROM type";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            types.Add(new ProductType()
                            {
                                TypeId = reader.GetInt32(reader.GetOrdinal("typeid")),
                                Type = reader.GetString(reader.GetOrdinal("type")),
                            });
                        }
                    }
                }
            }

            return types.FirstOrDefault();
        }

        public void Update(ProductType type)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int typeId)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "DELETE FROM type WHERE typeid = @typeid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("typeid", typeId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}