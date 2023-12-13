using System.Drawing;
using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
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
        
        
        public void AddColorToProduct(int productId, List<int> colorIds)
        {
            using (var con= _dbConnection.GetConnection())
            {
                con.Open();
                const string sql =
                    "INSERT INTO productcolors (productid, colorid) VALUES (@productid, @colorid)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    foreach (var colorId in colorIds)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@productid", productId);
                        command.Parameters.AddWithValue("@colorid", colorId);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void RemoveColorFromProduct(int productId, List<int> colorIds)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "Delete FROM productcolors WHERE productid = @productId AND colorid = @colorId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productId", productId);

                    foreach (var colorId in colorIds)
                    {
                        command.Parameters.AddWithValue("@colorId", colorId);
                        command.ExecuteNonQuery();
                        command.Parameters.Remove("@colorId");
                    }
                }
                
            }
        }

        public List<ColorType> GetColorsByProductId(int productId)
        {
            var colors = new List<ColorType>();

            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = @"SELECT c.colorId, c.color " +
                    "FROM productcolors pc " +
                    "JOIN colortype c ON pc.colorid = c.colorid " +
                    "WHERE pc.productid = @productid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@productId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var color = new ColorType
                            {
                                ColorId = reader.GetInt32(reader.GetOrdinal("colorid")),
                                Color = reader.GetString(reader.GetOrdinal("color")),
                            };
                            colors.Add(color);
                        }
                    }
                }
            }

            return colors;
        }

        public List<ColorType> GetAllColorTypes()
        {
            var colorTypes = new List<ColorType>();

            using (var con = _dbConnection.GetConnection())
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

            return colorTypes;
        }
    }
    
    
}