using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UserCreated { get; set; }

    public int? UserAssigned { get; set; }

    public int? CategoryId { get; set; }

    public int? PriorityId { get; set; }

    public int? StatusId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual TicketCategory? Category { get; set; }

    public virtual TicketPriority? Priority { get; set; }

    public virtual TicketStatus? Status { get; set; }

    public virtual ICollection<TicketAttachment> TicketAttachments { get; set; } = new List<TicketAttachment>();

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public virtual User? UserAssignedNavigation { get; set; }

    public virtual User? UserCreatedNavigation { get; set; }
}
