using System;
using System.Collections.Generic;

namespace BookingService.Models;

public partial class BookingStatusHistory
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
