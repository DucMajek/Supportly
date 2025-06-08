using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class TicketComment
{
    public int CommentId { get; set; }

    public int TicketId { get; set; }

    public int AuthorId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
