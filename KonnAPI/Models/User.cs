namespace KonnAPI.Models;

public class User {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Workspace> Workspaces { get; set; }
}
