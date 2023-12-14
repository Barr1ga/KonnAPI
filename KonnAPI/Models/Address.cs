using System.ComponentModel.DataAnnotations.Schema;

namespace KonnAPI.Models;

public class Address {

    public int Id { get; set; }

    [ForeignKey("Contact")]
    public int ContactId { get; set; }

    public string Location { get; set; }

    public Contact Contact { get; set; }

    public Boolean IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
