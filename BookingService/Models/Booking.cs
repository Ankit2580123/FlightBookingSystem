using System;
using System.Collections.Generic;

namespace BookingService.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FlightId { get; set; }

    public int Seats { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
}
