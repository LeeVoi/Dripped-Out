using System;
using System.Collections.Generic;
using System.Linq;
using infrastructure.Entities;
using Npgsql;
using service.Services;

namespace infrastructure.Repositories
{
    public class ColorTypeRepository : ICrud<ColorType>
    {

        private readonly DBConnection _dbConnection;

        public ColorTypeRepository(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public void Create(ColorType colorType)
        {
            using (var con = DBConnection.GetConnection())
            {
                con.Open();
                const string sql = "INSERT INTO colortype(color) VALUES (@color)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@color", colorType.Color);
                    command.ExecuteNonQuery();
                }
            }
        }

        //This method will READ ALL colors in the table
        public ColorType Read(int colorId)
        {
            var colorTypes = new List<ColorType>();

            using (var con = DBConnection.GetConnection())
            {
                con.Open();

                const string sql = "SELECT * FROM colortype";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colorTypes.Add(new ColorType()
                            {
                                ColorId = reader.GetInt32(reader.GetOrdinal("colorid")),
                                Color = reader.GetString(reader.GetOrdinal("color")),
                            });
                        }
                    }
                }
            }

            return colorTypes.FirstOrDefault();
        }

        [Obsolete("This method is not implemented and should not be called. Please remove any references to this method in your code.", true)]
        public void Update(ColorType colorType)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int colorId)
        {
            using (var con = DBConnection.GetConnection())
            {
                con.Open();

                const string sql = "DELETE FROM colortype WHERE colorid = @colorid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("colorId", colorId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}