namespace KonnAPI.Dto;

public class ContactDto {
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Pronounciation { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Telephone { get; set; }
    public string Image { get; set; }
    public string Notes { get; set; }
    public Boolean IsFavorite { get; set; }
    public Boolean IsBlocked { get; set; }
    public Boolean IsEmergency { get; set; }
    public Boolean IsTrashed { get; set; }
    public Boolean IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
