using System;
using Npgsql;
using System.Data;

namespace TestApp.WebApi.Data
{
    public class TestApiDbContext
    {
        public IConfiguration _config;
        private string _connectionString;

        public TestApiDbContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultParkingConnection");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}