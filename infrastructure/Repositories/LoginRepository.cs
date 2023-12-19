using infrastructure.DatabaseManager;
using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories
{
    public class LoginRepository : ICrud<PasswordHash>, ILoginMapper
    {
        private readonly IDBConnection _dbConnection;

        public LoginRepository(IDBConnection dbConnection)
        {
            _dbConnection = new DBConnection();
        }
        public PasswordHash Create(PasswordHash passwordHash)
        {
            using (var con= _dbConnection.GetConnection())
            {
                con.Open();
                const string sql =
                    "INSERT INTO passwordhash(userid, hash, salt, algorithm) values (@userid, @hash, @salt, @algorithm)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@userid", passwordHash.UserId);
                    command.Parameters.AddWithValue("@hash", passwordHash.Hash);
                    command.Parameters.AddWithValue("@salt", passwordHash.Salt);
                    command.Parameters.AddWithValue("@algorithm", passwordHash.Algorithm);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PasswordHash
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                                Hash = (byte[])reader["hash"],
                                Salt = (byte[])reader["salt"],
                                Algorithm = reader.GetString(reader.GetOrdinal("algorithm"))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public PasswordHash Read(int userId)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "SELECT * FROM passwordhash WHERE userid = @userid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@userid", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PasswordHash
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                                Hash = (byte[])reader["hash"],
                                Salt = (byte[])reader["salt"],
                                Algorithm = reader.GetString(reader.GetOrdinal("algorithm"))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Update(PasswordHash passwordHash)
        {
            using (var con = _dbConnection.GetConnection())
            {
                con.Open();
                const string sql = "UPDATE passwordhash SET hash = @hash, salt = @salt, algorithm = @algorithm WHERE userid = @userid";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@hash", passwordHash.Hash);
                    command.Parameters.AddWithValue("@salt", passwordHash.Salt);
                    command.Parameters.AddWithValue("@algorithm", passwordHash.Algorithm);
                    command.Parameters.AddWithValue("@userid", passwordHash.UserId);
                    command.ExecuteNonQuery();
                }
            }
        }

        [Obsolete("This method is not implemented and should not be called. Please remove any references to this method in your code.", true)]
        public void Delete(int userid)
        {
            throw new System.NotImplementedException();
        }
        

        public Users GetUserByEmail(string email)
        {
            using (var conn = _dbConnection.GetConnection())
            {
                conn.Open();
                const string sql = "SELECT * FROM users WHERE email = @email";
                
                using (var command = new NpgsqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("isadmin"))
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
