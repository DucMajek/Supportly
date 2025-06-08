using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public virtual ICollection<Ticket> TicketUserAssignedNavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketUserCreatedNavigations { get; set; } = new List<Ticket>();
}
