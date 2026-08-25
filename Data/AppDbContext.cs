using CinemaTicketBookingProject.Data.Configurations;
using CinemaTicketBookingProject.Model;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieConfigurations());
            modelBuilder.ApplyConfiguration(new AuditoriumConfigurations());
            modelBuilder.ApplyConfiguration(new ShowTimeConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerConfigurations());
            modelBuilder.ApplyConfiguration(new BookingConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
