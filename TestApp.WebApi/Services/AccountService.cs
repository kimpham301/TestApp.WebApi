using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TestApp.WebApi.Services;
using TestApp.WebApi.Models;
using TestApp.WebApi.Repository;

namespace TestApp.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var userLogin = await _accountRepository.UserLogin(model);
            if (userLogin == null) return null;

            var token = CreateToken(userLogin);
            return new AuthenticateResponse(userLogin, token);
        }

        public async Task<int> Register(Account newAccount)
        {
            var userRegister = await _accountRepository.Register(newAccount);
            return userRegister;
        }

        public async Task<Account> ExistedEmail(string email)
        {
            var userEmail = await _accountRepository.ExistedEmail(email);
            return userEmail;
        }

        public async Task UpdateAccount(int id, Result result)
        {
            await _accountRepository.UpdateAccount(id, result);
            
        }

        private string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.email)
            };
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}