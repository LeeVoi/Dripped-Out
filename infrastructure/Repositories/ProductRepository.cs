using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;
namespace infrastructure.Repositories
{
    public class ProductRepository : ICrud<Products>
    {
        public readonly IDBConnection _dbConnection;

        public ProductRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public void Create(Products products)
        {
            using (var con= _dbConnection.GetConnection())
            {
                con.Open();
                const string sql =
                    "INSERT INTO product(productname, typeid, price, gender, description) values (@productname, @typeid, @price, @gender, @description)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productname", products.ProductName);
                    command.Parameters.AddWithValue("@typeid",products.TypeId);
                    command.Parameters.AddWithValue("@price",products.Price);
                    command.Parameters.AddWithValue("@gender", products.Gender);
                    command.Parameters.AddWithValue("@description", products.Description);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Products Read(int productId)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "SELECT * FROM Products WHERE productid = @productid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Products
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("productid")),
                                ProductName = reader.GetString(reader.GetOrdinal("productname")),
                                TypeId = reader.GetInt32(reader.GetOrdinal("typeid")),
                                Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                Gender = reader.GetString(reader.GetOrdinal("gender")),
                                Description = reader.GetString(reader.GetOrdinal("description"))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Update(Products products)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql =
                    "UPDATE Products SET productname = @productname, typeid = @typeId, price = @price, gender = @gender, description = @description WHERE productid = @ProductId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productId", products.ProductId);
                    command.Parameters.AddWithValue("@productName", products.ProductName);
                    command.Parameters.AddWithValue("@typeId", products.TypeId);
                    command.Parameters.AddWithValue("@price", products.Price);
                    command.Parameters.AddWithValue("@gender", products.Gender);
                    command.Parameters.AddWithValue("@description", products.Description);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int productId)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "DELETE FROM Products WHERE productid = @productId";

                using (var command = new NpgsqlCommand())
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}