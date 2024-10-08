namespace Common.Entities;

public class Common
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
}