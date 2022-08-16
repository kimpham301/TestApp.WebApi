using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestApp.WebApi.Authorization;
using TestApp.WebApi.Models;
using TestApp.WebApi.Repository;

namespace TestApp.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public UsersController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin(Account account)
        {
            try
            {
                var userLogin = await _accountRepository.UserLogin(account.email, account.password);
                if (userLogin == null)
                {
                    return BadRequest("Email or password is incorret");
                }

                string token = CreateToken(account);
                return Ok(userLogin + token);
            }
            catch(Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> UserRegister(Account account)
        {
            try
            {
                var existedEmail = await _accountRepository.ExistedEmail(account.email);
                if (existedEmail != null)
                {
                    return BadRequest("Email is already existed");
                }
                var userRegister = await _accountRepository.Register(account);
                return Ok("User is successfully registered");
            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }
        private string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

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
