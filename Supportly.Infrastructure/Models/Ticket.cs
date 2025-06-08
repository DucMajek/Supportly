using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserCreated { get; set; }

    public int UserAssigned { get; set; }

    public int CategoryId { get; set; }

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual TicketCategory Category { get; set; } = null!;

    public virtual TicketPriority Priority { get; set; } = null!;

    public virtual TicketStatus Status { get; set; } = null!;

    public virtual ICollection<TicketAttachment> TicketAttachments { get; set; } = new List<TicketAttachment>();

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public virtual User UserAssignedNavigation { get; set; } = null!;

    public virtual User UserCreatedNavigation { get; set; } = null!;
}
