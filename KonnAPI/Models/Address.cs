using System.ComponentModel.DataAnnotations;

namespace KonnAPI.Models;

public class Address
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ContactId { get; set; }

    public string FullName { get; set; } = string.Empty;

    [Required]
    public bool IsDefault { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public virtual Contact Contact { get; set; }
}
