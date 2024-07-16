using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public string Title { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public string? Image { get; set; }

    public string Notes { get; set; } = string.Empty;

    public Boolean IsFavorite { get; set; } = false;

    public Boolean IsBlocked { get; set; } = false;

    public Boolean IsEmergency { get; set; } = false;

    public Boolean IsDeleted { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; }

    public virtual ICollection<Social> Socials { get; set; }

    public virtual ICollection<ContactCategory> ContactCategories { get; set; }

    public virtual Workspace Workspace { get; set; }
}
