using System.ComponentModel.DataAnnotations;

namespace KonnAPI.Models;

public class Contact
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int WorkspaceId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Pronounciation { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public string? Image { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsFavorite { get; set; } = false;

    public bool IsBlocked { get; set; } = false;

    public bool IsEmergency { get; set; } = false;

    public bool IsDeleted { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<ContactCategory> ContactCategories { get; set; } = new List<ContactCategory>();

    public virtual Workspace Workspace { get; set; }
}
