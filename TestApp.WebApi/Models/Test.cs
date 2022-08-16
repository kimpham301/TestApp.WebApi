namespace TestApp.WebApi.Models;

public class Test
{
    public int test_id { get; set; }
    public string question { get; set; }
    public List<string> Options { get; set; }
    public int answer { get; set; }
}