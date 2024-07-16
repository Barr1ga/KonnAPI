using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KonnAPI.Models;

public enum SocialType
{
    LinkedIn,
    Github
}

public class Social
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int ContactId { get; set; }
    public SocialType? Type { get; set; }
    public string Url { get; set; } = string.Empty;
    public virtual Contact Contact { get; set; }
}
