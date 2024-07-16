using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KonnAPI.Models;

public class Category
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int WorkspaceId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    [Required]
    public Boolean IsDeleted { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public virtual Workspace Workspace { get; set; }

    public virtual ICollection<ContactCategory> ContactCategories { get; set; }
}
