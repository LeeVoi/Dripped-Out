using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories
{
    public class ColorTypeRepository : ICrud<ColorType>
    {

        private readonly IDBConnection _dbConnection;

        public ColorTypeRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public void Create(ColorType colorType)
        {
            using (var con = _dbConnection.GetConnection())
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
        
        public ColorType Read(int colorId)
        {
            var colorType = new ColorType();

            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "SELECT * FROM colortype WHERE colorid = @colorId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@ColorId", colorId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colorType.ColorId = reader.GetInt32(reader.GetOrdinal("colorId"));
                            colorType.Color = reader.GetString(reader.GetOrdinal("color"));
                        }
                    }
                }
            }

            return colorType;
        }

        [Obsolete("This method is not implemented and should not be called. Please remove any references to this method in your code.", true)]
        public void Update(ColorType colorType)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int colorId)
        {
            using (var con = _dbConnection.GetConnection())
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