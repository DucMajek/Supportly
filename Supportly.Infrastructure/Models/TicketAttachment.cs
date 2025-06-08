using System;
using System.Collections.Generic;

namespace Supportly.Infrastructure.Models;

public partial class TicketAttachment
{
    public int AttachmentId { get; set; }

    public int TicketId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
