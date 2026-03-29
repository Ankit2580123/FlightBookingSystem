using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Models;

public partial class BookingDbContext : DbContext
{
    public BookingDbContext()
    {
    }

    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingPassenger> BookingPassengers { get; set; }

    public virtual DbSet<BookingStatusHistory> BookingStatusHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-5A0VF6I\\SQLEXPRESS;Database=BookingDb;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC07DF5C062E");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<BookingPassenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingP__3214EC07D161D197");

            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PassengerName).HasMaxLength(100);
        });

        modelBuilder.Entity<BookingStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingS__3214EC076201D3D8");

            entity.ToTable("BookingStatusHistory");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
