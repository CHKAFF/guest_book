using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Models.Requests;

public class CreateGuestRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Message { get; set; }
}