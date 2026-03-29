using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Models;

public partial class Flight
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FlightNumber { get; set; } = null!;

    [StringLength(50)]
    public string Source { get; set; } = null!;

    [StringLength(50)]
    public string Destination { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DepartureTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ArrivalTime { get; set; }

    public int TotalSeats { get; set; }

    public int AvailableSeats { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
