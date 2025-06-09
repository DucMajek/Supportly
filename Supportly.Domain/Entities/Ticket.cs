using Supportly.Domain.Enums;

namespace Supportly.Domain.Entities;

public class Ticket
{
    public int TicketId { get; set; }
    public string? Title { get; set; }
    public int UserIdCreated { get; set; }
    public int UserIdAssigned { get; set; }
    public Category Category { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public bool IsDeleted { get; set; }
}