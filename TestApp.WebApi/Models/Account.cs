namespace TestApp.WebApi.Models;

public class Account
{
    public int user_id { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int roles { get; set; }
    
}
public class Result
{
    public int user_id { get; set; }
    public int score { get; set; }
    public int TimeTaken { get; set; }

}