namespace Supportly.Domain.Entities;

public class TicketComment
{
    public int CommentId { get; set; }
    public int TicketId { get; set; }
    public int AuthorId { get; set; }
    public string? Content { get; set; }
    public bool IsDeleted { get; set; }
}