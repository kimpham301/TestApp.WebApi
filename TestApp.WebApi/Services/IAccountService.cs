using TestApp.WebApi.Models;

namespace TestApp.WebApi.Services;

public interface IAccountService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<int> Register(Account newAccount);
    Task<Account> ExistedEmail(string email);
    public Task<int> Load_Result(Result result);
}