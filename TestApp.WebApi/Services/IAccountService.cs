using TestApp.WebApi.Models;

namespace TestApp.WebApi.Services;

public interface IAccountService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<int> Register(Account newAccount);
    Task<Account> ExistedEmail(string email);
    Task<Account> GetUser(int id);
    Task UpdateAccount(int id, Result result);
}