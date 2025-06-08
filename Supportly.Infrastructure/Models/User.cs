using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public virtual ICollection<Ticket> TicketUserAssignedNavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketUserCreatedNavigations { get; set; } = new List<Ticket>();
}
