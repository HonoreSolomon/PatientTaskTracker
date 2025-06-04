using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.TaskId);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Created).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(t => t.IsCompleted).HasDefaultValue(false);
        }
    }
}
