using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class TicketCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
