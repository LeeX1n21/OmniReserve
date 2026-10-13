using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmniReserve.Domain.Entities;

namespace OmniReserve.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomId)
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.CheckInDate)
            .IsRequired();

        builder.Property(r => r.CheckOutDate)
            .IsRequired();

        builder.Property(r => r.TotalPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(reservation => reservation.Room)
               .WithMany(room => room.Reservations)
               .HasForeignKey(reservation => reservation.RoomId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(reservation => reservation.User)
               .WithMany(user => user.Reservations)
               .HasForeignKey(reservation => reservation.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}