namespace KonnAPI.Models;

public class Workspace {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public virtual ICollection<Category> Categories { get; set; }

    public Boolean IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
