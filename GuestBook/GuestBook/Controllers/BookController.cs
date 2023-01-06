using GuestBook.DB;
using GuestBook.Models;
using GuestBook.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers;

[Route("api/book")]
public class BookController : ControllerBase
{
    private GuestBookDB guestBookDb;
    public BookController(GuestBookDB guestBookDb)
    {
        this.guestBookDb = guestBookDb;
    }
    
    [HttpGet]
    public async Task<ActionResult<Guest[]>> GetAsync()
    {
        return await guestBookDb.GetAll();
    }

    [HttpPost("guests")]
    public async Task<ActionResult<Guest>> CreateGuestAsync(CreateGuestRequest createGuestRequest)
    {
        return await guestBookDb.Create(createGuestRequest);
    }
}