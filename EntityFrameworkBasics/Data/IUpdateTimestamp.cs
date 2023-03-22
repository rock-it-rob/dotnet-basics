namespace EntityFrameworkBasics.Data;

public interface IUpdateTimestamp
{
    public DateTime? Updated { get; set; }
}