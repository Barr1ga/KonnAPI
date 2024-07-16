namespace KonnAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Boolean IsDeleted { get; set; } = false;
    public virtual ICollection<Workspace> Workspaces { get; set; }
}
