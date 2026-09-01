using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTen.Enums;
using TaskTen.Model;

namespace TaskTen.Data.Entities
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> userEntity)
        {
            userEntity.HasKey(u => u.Id);
            userEntity.Property(u => u.Name).IsRequired().HasMaxLength(200);
            userEntity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            userEntity.Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            userEntity.Property(u => u.Role).HasDefaultValue(Role.User);
        }
    }
}