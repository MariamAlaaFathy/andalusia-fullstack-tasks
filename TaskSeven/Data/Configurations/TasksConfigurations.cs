using FullStackSession6.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskSeven.Data.Entities
{
    public class TasksConfigurations : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> taskEntity)
        {
            taskEntity.HasKey(t => t.Id);
            taskEntity.Property(t => t.Title).IsRequired().HasMaxLength(50);
            taskEntity.Property(t => t.IsCompleted).HasDefaultValue(false);
            taskEntity.Property(t => t.TaskStatus).HasDefaultValue("Pending").HasMaxLength(50);
            taskEntity.Property(t => t.DueDate).IsRequired();
            taskEntity.Property(t => t.CreatedAt).HasDefaultValueSql("GETDATE()");

            taskEntity.HasOne(t => t.User).WithMany(u => u.Tasks).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
