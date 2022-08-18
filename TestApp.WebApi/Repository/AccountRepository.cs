﻿using TestApp.WebApi.Models;
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

        public async Task<Account> UserLogin(AuthenticateRequest account)
        {
            string hashedPassword = Password.hashPassword(account.password);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Account>(
                    $@"SELECT * FROM Accounts WHERE email = '{account.email}' AND password = '{hashedPassword}'");
                return user;
            }
        }

        public async Task<int> Register(Account newAccount)
        {
            string hashedPassword = Password.hashPassword(newAccount.password);
            var query = "INSERT INTO Accounts (email, password, roles) VALUES (@email, @password, @roles)";

            var parameters = new DynamicParameters();
            parameters.Add("email", newAccount.email, DbType.String);
            parameters.Add("password", hashedPassword, DbType.String);
            parameters.Add("roles", 1, DbType.Int32);

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

        public async Task<Account> GetUser(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var getUser =
                    await connection.QueryFirstOrDefaultAsync<Account>(
                        $@"SELECT * FROM Accounts WHERE user_id = '{id}'");
                return getUser;
            }
        }

        public async Task UpdateAccount(int id, Result result)
        {
            var query = "UPDATE Accounts SET timetaken = @timetaken, score = @score WHERE user_id = @user_id";
            var parameters = new DynamicParameters();
            parameters.Add("user_id", result.user_id, DbType.Int32);
            parameters.Add("timeTaken", result.timetaken, DbType.Int32);
            parameters.Add("score", result.score, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
