using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class TicketPriority
{
    public int PriorityId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
