namespace KonnAPI.Models;

public enum SocialType {
    LinkedIn,
    Github
}

public class Social {
    public int Id { get; set; }

    public int ContactId { get; set; }

    public SocialType Type { get; set; }

    public string Url { get; set; }

    public Contact Contact { get; set; }
}
