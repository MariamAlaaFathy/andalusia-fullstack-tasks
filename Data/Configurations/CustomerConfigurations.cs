using CinemaTicketBookingProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingProject.Data.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> customerEntity)
        {
            customerEntity.HasKey(c => c.Id);
            customerEntity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            customerEntity.Property(c => c.Email).IsRequired().HasMaxLength(100);
            customerEntity.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
            customerEntity.Property(c => c.UpdatedAt);
        }
    }
}
