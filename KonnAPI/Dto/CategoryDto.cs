namespace KonnAPI.Dto;

public class CategoryDto
{
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CategoryCreateDto
{
    public int WorkspaceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
