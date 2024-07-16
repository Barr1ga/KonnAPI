namespace KonnAPI.Dto;

public class ContactDto
{
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Pronounciation { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Telephone { get; set; }
    public string Image { get; set; }
    public string Notes { get; set; }
    public bool IsFavorite { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsEmergency { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ContactCreateDto
{
    public int WorkspaceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Pronounciation { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Telephone { get; set; }
    public string Image { get; set; }
    public string Notes { get; set; }
    public bool IsFavorite { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsEmergency { get; set; }
}
