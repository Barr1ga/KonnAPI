using System.ComponentModel.DataAnnotations;

namespace KonnAPI.Dto;

public class MessageDto
{
    public string Status { get; set; }
    public string Message { get; set; } = string.Empty;

    public MessageDto(string status, string message)
    {
        Status = status;
        Message = message;
    }
}