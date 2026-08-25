using CinemaTicketBookingProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingProject.Data.Configurations
{
    public class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> movieEntity)
        {
            movieEntity.HasKey(m => m.Id);
            movieEntity.Property(m => m.Name).IsRequired().HasMaxLength(200);
            movieEntity.Property(m => m.Genre).IsRequired().HasMaxLength(200);
            movieEntity.Property(m => m.ReleaseDate).IsRequired();
            movieEntity.Property(m => m.AvailableInCinema).IsRequired();
            movieEntity.Property(m => m.CreatedAt).HasDefaultValueSql("GETDATE()");
            movieEntity.Property(m => m.UpdatedAt);
        }
    }
}
