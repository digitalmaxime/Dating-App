using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }
    
    // Navigation properties (required)
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;
}