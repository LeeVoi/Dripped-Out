using System;
using System.Collections.Generic;
using System.Linq;
using infrastructure.Entities;
using Npgsql;
using service.Services;

namespace infrastructure.Repositories
{
    public class SizeTypeRepository : ICrud<SizeType>
    {
        private readonly DBConnection _dbConnection;

        public SizeTypeRepository(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Create(SizeType sizeType)
        {
            using (var con = DBConnection.GetConnection())
            {
                con.Open();
                const string sql = "INSERT INTO sizetype(size) VALUES (@size)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@color", sizeType.Size);
                    command.ExecuteNonQuery();
                }
            }
        }

        //This method will READ ALL sizes in the table
        public SizeType Read(int sizeId)
        {
            var sizeTypes = new List<SizeType>();

            using (var con = DBConnection.GetConnection())
            {
                con.Open();

                const string sql = "SELECT * FROM sizetype";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sizeTypes.Add(new SizeType()
                            {
                                SizeId = reader.GetInt32(reader.GetOrdinal("sizeid")),
                                Size = reader.GetString(reader.GetOrdinal("size")),
                            });
                        }
                    }
                }
            }

            return sizeTypes.FirstOrDefault();
        }
        
        [Obsolete("This method is not implemented and should not be called. Please remove any references to this method in your code.",true)]
        public void Update(SizeType sizeType)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int sizeId)
        {
            using (var con = DBConnection.GetConnection())
            {
                con.Open();

                const string sql = "DELETE FROM sizetype WHERE sizeid = @sizeid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("sizeId", sizeId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}