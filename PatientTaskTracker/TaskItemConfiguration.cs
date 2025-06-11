using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PatientTaskTracker
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.TaskId);

            builder.Property(t => t.TaskId)
                .ValueGeneratedOnAdd();


            builder.Property(t => t.Description).IsRequired().HasMaxLength(500);

            builder.Property(t => t.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.IsCompleted).HasDefaultValue(false);

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.HasOne(t => t.Patient)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
