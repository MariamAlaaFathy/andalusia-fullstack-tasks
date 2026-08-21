using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEight.Model;

namespace TaskEight.Data.Entities
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> userEntity)
        {
            userEntity.HasKey(u => u.Id);
            userEntity.Property(u => u.Name).IsRequired().HasMaxLength(50);
        }
    }
}