namespace TestApp.WebApi.Models;

public class AuthenticateResponse
{
    public int user_id { get; set; }
    public string email { get; set; }
    public string Token { get; set; }
    public int roles_id { get; set; }

    public AuthenticateResponse(Account account, string token)
    {
        user_id = account.user_id;
        email = account.email;
        roles_id = account.roles;
        Token = token;
    }
}