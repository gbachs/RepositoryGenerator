using System.Data.SqlClient;

namespace RepositoryGenerator.Core.Extensions.Database
{
    public class SqlDatabase
    {
        private readonly string _connectionString;

        public SqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}