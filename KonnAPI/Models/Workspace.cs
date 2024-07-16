namespace KonnAPI.Models;

public class Workspace
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public Boolean IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Contact> Contacts { get; set; }
}
