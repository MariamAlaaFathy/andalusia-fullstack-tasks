using CinemaTicketBookingProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingProject.Data.Configurations
{
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> bookingEntity)
        {
            bookingEntity.HasKey(b => b.Id);
            bookingEntity.Property(b => b.BookingDate).IsRequired();
            bookingEntity.Property(b => b.Status).IsRequired();
            bookingEntity.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            bookingEntity.Property(b => b.UpdatedAt);

            bookingEntity.HasOne(b => b.Customer).WithMany(c => c.Bookings).HasForeignKey(b => b.CustomerId).OnDelete(DeleteBehavior.Cascade);
            bookingEntity.HasOne(b => b.ShowTime).WithMany(s => s.Bookings).HasForeignKey(b => b.ShowTimeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
