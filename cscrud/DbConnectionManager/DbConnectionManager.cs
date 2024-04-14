using MySql.Data.MySqlClient;

namespace Cscrud
{
    public class DbConnectionManager
    {
        private readonly string _connectionString;

        public DbConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
