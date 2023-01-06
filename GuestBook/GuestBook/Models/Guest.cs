namespace GuestBook.Models;

public class Guest
{
    public ulong? Id { get; set; }
    public string? Name { get; set; }
    public string? Message { get; set; }
    public DateTime? AddedDate { get; set; }
}