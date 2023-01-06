using System.ComponentModel.DataAnnotations;

namespace GuestBook.Models.Requests;

public class CreateGuestRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Message { get; set; }
}