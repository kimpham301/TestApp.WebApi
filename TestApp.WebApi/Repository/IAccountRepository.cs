using TestApp.WebApi.Models;

namespace TestApp.WebApi.Repository
{
    public interface IAccountRepository
    {
        public Task<Account> UserLogin(string email, string password);
        public Task<int> Register(Account newAccount);

        public Task<Account> ExistedEmail(string email);
    }
}
