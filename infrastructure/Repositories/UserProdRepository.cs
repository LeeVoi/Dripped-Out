using System;
using System.Collections.Generic;

using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using Npgsql;

namespace infrastructure.Repositories
{
    public class UserProdRepository
    {
        private readonly IDBConnection _dbConnection;
        
        public UserProdRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Products> GetUserLikes (Users userid)
        {
            using (var con =_dbConnection.GetConnection())
            {
                con.Open();
                
                const string query =
                    @"SELECT product.productid, product.productname, product.typeid, product.price FROM product
INNER JOIN userlikes ul on product.productid = ul.productid
INNER JOIN users u on ul.userid = u.userid
WHERE u.userid = @userid";

                using (var command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@userid", userid);

                    using (var reader = command.ExecuteReader())
                    {
                        var products = new List<Products>();

                        while (reader.Read())
                        {
                            products.Add(new Products
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                            });
                        }

                        return products;
                    }
                }
                
            }
        }

        public List<Products> GetUserCart(Users userid)
        {
            using (var con =_dbConnection.GetConnection())
            {
                con.Open();
                
                const string query =
                    @"SELECT product.productid, product.productname, product.typeid, product.price FROM product
INNER JOIN userCart uc on product.productid = uc.productid
INNER JOIN users u on uc.userid = u.userid
WHERE u.userid = @userid";

                using (var command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@userid", userid);

                    using (var reader = command.ExecuteReader())
                    {
                        var products = new List<Products>();

                        while (reader.Read())
                        {
                            products.Add(new Products
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                            });
                        }

                        return products;
                    }
                }
                
            }
        }
    }
}