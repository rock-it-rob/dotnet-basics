namespace ApiBasics.Model;

public class Diagnostic
{
    public string? Environment { get; set; }
    public DateTime AsOf { get; set; } = DateTime.Now;
}