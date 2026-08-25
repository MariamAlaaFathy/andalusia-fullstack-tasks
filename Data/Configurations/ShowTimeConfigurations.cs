using CinemaTicketBookingProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingProject.Data.Configurations
{
    public class ShowTimeConfigurations : IEntityTypeConfiguration<ShowTime>
    {
        public void Configure(EntityTypeBuilder<ShowTime> showTimeEntity)
        {
            showTimeEntity.HasKey(s => s.Id);
            showTimeEntity.Property(s => s.Show_Time).IsRequired();
            showTimeEntity.Property(s => s.CreatedAt).HasDefaultValueSql("GETDATE()");
            showTimeEntity.Property(s => s.UpdatedAt);

            showTimeEntity.HasOne(s => s.Movie).WithMany(m => m.ShowTimes).HasForeignKey(m => m.MovieId).OnDelete(DeleteBehavior.Cascade);
            showTimeEntity.HasOne(s => s.Auditorium).WithMany(m => m.Shows).HasForeignKey(m => m.AuditoriumId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
