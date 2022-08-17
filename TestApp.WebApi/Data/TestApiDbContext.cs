using Npgsql;
using System.Data;

namespace TestApp.WebApi.Data
{
    public class TestApiDbContext
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public TestApiDbContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultParkingConnection");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}