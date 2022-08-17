using Microsoft.AspNetCore.Mvc;
using TestApp.WebApi.Models;

using TestApp.WebApi.Services;

namespace TestApp.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin(AuthenticateRequest account)
        {
            try
            {
                var userLogin = await _accountService.Authenticate(account);
                if (userLogin == null)
                {
                    return BadRequest("Email or password is incorrect");
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
                var existedEmail = await _accountService.ExistedEmail(account.email);
                if (existedEmail != null)
                {
                    return BadRequest("Email is already existed");
                }

                var userRegister = await _accountService.Register(account);
                if (userRegister == null)
                {
                    return BadRequest("There's something wrong");
                }
                return Ok("User is successfully registered");
            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, Result result)
        {
            try
            {
                await _accountService.UpdateAccount(id, result);
                return NoContent();
            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }
        
    }
}
