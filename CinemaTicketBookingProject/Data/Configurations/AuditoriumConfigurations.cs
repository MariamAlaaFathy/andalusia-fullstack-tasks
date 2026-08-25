using CinemaTicketBookingProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingProject.Data.Configurations
{
    public class AuditoriumConfigurations : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> auditoriumEntity)
        {
            auditoriumEntity.HasKey(a => a.Id);
            auditoriumEntity.Property(a => a.RoomNumber).IsRequired();
            auditoriumEntity.Property(a => a.Capacity).IsRequired();
            auditoriumEntity.Property(a => a.Available).IsRequired();
            auditoriumEntity.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");
            auditoriumEntity.Property(a => a.UpdatedAt);
        }
    }
}
