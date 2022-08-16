using System.ComponentModel.DataAnnotations;

namespace TestApp.WebApi.Models;

public class AuthenticateRequest
{
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
}