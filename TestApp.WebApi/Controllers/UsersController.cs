using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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

                return Ok(userLogin);
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
    }
}
