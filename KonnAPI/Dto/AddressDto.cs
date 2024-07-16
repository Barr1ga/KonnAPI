namespace KonnAPI.Dto;

public class AddressDto
{
    public int Id { get; set; }
    public int ContactId { get; set; }
    public string FullName { get; set; }
    public bool IsDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AddressCreateDto
{
    public int ContactId { get; set; }
    public string FullName { get; set; }
    public bool IsDefault { get; set; }
}