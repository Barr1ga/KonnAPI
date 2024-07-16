using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Models;

public class User
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Image { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    [Required]
    public Boolean IsDeleted { get; set; } = false;

    public virtual ICollection<Workspace> Workspaces { get; set; }
}
