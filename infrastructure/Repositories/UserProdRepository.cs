﻿using System;
using System.Collections.Generic;
using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories
{
    public class UserProdRepository : IUserProdMapper
    {
        private readonly IDBConnection _dbConnection;
        
        public UserProdRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public List<Products> GetUserLikes(int UserId)
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
                    command.Parameters.AddWithValue("@userid", UserId);

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
                                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                            });
                        }

                        return products;
                    }
                }
                
            }
        }

        public List<Products> GetUserCart(int UserId)
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
                    command.Parameters.AddWithValue("@userid", UserId);

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