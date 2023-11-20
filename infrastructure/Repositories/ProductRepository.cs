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
                    "INSERT INTO product(productname, rating, price, gender) values (@productname, @rating, @price, @gender)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productname", products.ProductName);
                    command.Parameters.AddWithValue("@rating", products.Rating);
                    command.Parameters.AddWithValue("@price",products.Price);
                   // command.Parameters.AddWithValue("@gender", products.Gender);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Products Read(int productId)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "SELECT * FROM Products WHERE ProductId = @Productid";

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
                                Rating = reader.GetInt32(reader.GetOrdinal("rating")),
                                Price = reader.GetDecimal(reader.GetOrdinal("price"))
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
                    "UPDATE Products SET productname = @Productname, typeid = @TypeId, rating = @Rating, price = @Price WHERE productId = @ProductId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@ProductId", products.ProductId);
                    command.Parameters.AddWithValue("@ProductName", products.ProductName);
                    command.Parameters.AddWithValue("@TypeId", products.TypeId);
                    command.Parameters.AddWithValue("@Rating", products.Rating);
                    command.Parameters.AddWithValue("@Price", products.Price);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int productId)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "DELETE FROM Products WHERE productId = @ProductId";

                using (var command = new NpgsqlCommand())
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}