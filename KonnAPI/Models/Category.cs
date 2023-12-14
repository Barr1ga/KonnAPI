namespace KonnAPI.Models;

public class Category {

    public int Id { get; set; }

    public int WorkspaceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Workspace Workspace { get; set; }

    public Boolean IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ContactCategory> ContactCategories { get; set; }
}
