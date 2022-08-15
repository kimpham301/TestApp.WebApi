using TestApp.WebApi.Models;
using TestApp.WebApi.Data;
using TestApp.WebApi.Authorization;
using System.Data;
using Dapper;

namespace TestApp.WebApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TestApiDbContext _context;
        public AccountRepository(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<Account> UserLogin(string email, string password)
        {
            string hashedPassword = Password.hashPassword(password);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Account>(
                    $@"SELECT * FROM Accounts WHERE email = '{email}' AND password = '{hashedPassword}'");
                return user;
            }
        }

        public async Task<int> Register(Account newAccount)
        {
            string hashedPassword = Password.hashPassword(newAccount.password);
            var query = "INSERT INTO Accounts (email, password) VALUES (@email, @password)";

            var parameters = new DynamicParameters();
            parameters.Add("email", newAccount.email, DbType.String);
            parameters.Add("password", hashedPassword, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Account> ExistedEmail(string email)
        {
            using (var connection = _context.CreateConnection())
            {
                var userEmail = await connection.QuerySingleOrDefaultAsync<Account>(
                    $@"SELECT * FROM Accounts WHERE email = '{email}'");
                return userEmail;
            }
        }
    }

}
