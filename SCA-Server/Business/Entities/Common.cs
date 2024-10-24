using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities;

public abstract class Common
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}