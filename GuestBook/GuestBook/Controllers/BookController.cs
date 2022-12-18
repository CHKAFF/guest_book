using GuestBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers;

[Route("api/book")]
public class BookController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Guest[]>> GetAsync()
    {
        return Ok(Array.Empty<Guest>());
    }

    [HttpPost("guests")]
    public async Task<ActionResult<Guest>> CreateGuestAsync()
    {
        return Ok(
            new Guest
            {
                Id = Guid.NewGuid(),
                Name = "Ivan Ivanovich",
                Message = "Всем привет",
                AddedDate = DateTime.Now
            });
    }
}