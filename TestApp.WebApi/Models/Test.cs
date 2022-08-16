namespace TestApp.WebApi.Models;

public class Test
{
    public int question_id { get; set; }
    public string question { get; set; }
    public string[] Options { get; set; }
    public int answer { get; set; }
}