namespace KonnAPI.Models;

public class ContactCategory {
    public int Id { get; set; }
    public int ContactId { get; set; }
    public int CategoryId { get; set; }
    public virtual Contact Contact { get; set; }
    public virtual Category Category { get; set; }
}
