using TestApp.WebApi.Models;

namespace TestApp.WebApi.Repository
{
    public interface IAccountRepository
    {
        public Task<Account> UserLogin(AuthenticateRequest account);
        public Task<int> Register(Account newAccount);

        public Task<Account> ExistedEmail(string email);
        public Task<Result> Load_Result(Result result);
    }
}
