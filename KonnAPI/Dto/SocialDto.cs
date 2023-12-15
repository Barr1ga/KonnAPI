using KonnAPI.Models;

namespace KonnAPI.Dto;

public class SocialDto {
    public int Id { get; set; }
    public int ContactId { get; set; }
    public SocialType Type { get; set; }
    public string Url { get; set; }
}
