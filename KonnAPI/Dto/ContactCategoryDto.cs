namespace KonnAPI.Dto;

public class ContactCategoryDto
{
    public int Id { get; set; }
    public int ContactId { get; set; }
    public int CategoryId { get; set; }
}

public class ContactCategoryCreateDto
{
    public int ContactId { get; set; }
    public int CategoryId { get; set; }
}

