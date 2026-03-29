using System;
using System.Collections.Generic;

namespace BookingService.Models;

public partial class BookingPassenger
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public string? PassengerName { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }
}
