using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;
namespace infrastructure.Repositories
{
    public class SizeTypeRepository : ICrud<SizeType>
    {
        private readonly IDBConnection _dbConnection;

        public SizeTypeRepository(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Create(SizeType sizeType)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "INSERT INTO sizetype (size) VALUES (@size)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@size", sizeType.Size);
                    command.ExecuteNonQuery();
                }
            }
        }

        //This method will READ ALL sizes in the table
        public SizeType Read(int sizeId)
        {
            var sizeTypes = new SizeType();

            using (var con = _dbConnection.GetConnection())
            {
                con.Open();

                const string sql = "SELECT * FROM sizetype WHERE sizeid = @sizeId";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@sizeId", sizeId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sizeTypes.SizeId = reader.GetInt32(reader.GetOrdinal("sizeId"));
                            sizeTypes.Size = reader.GetString(reader.GetOrdinal("size"));
                        }
                    }
                }
            }
            return sizeTypes;
        }
        
        [Obsolete("This method is not implemented and should not be called. Please remove any references to this method in your code.",true)]
        public void Update(SizeType sizeType)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int sizeId)
        {
            using (var con = _dbConnection.GetConnection())
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