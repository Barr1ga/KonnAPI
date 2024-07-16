namespace KonnAPI.Dto;

public class AddressDto
{
    public int Id { get; set; }
    public int ContactId { get; set; }
    public string Location { get; set; }
    public bool IsDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
