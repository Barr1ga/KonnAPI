using System.ComponentModel.DataAnnotations;

namespace KonnAPI.Models;

public class ContactCategory
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ContactId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public virtual Contact Contact { get; set; }

    public virtual Category Category { get; set; }
}
