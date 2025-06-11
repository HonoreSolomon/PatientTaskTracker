using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId);

            builder.Property(p => p.PatientId)
                .ValueGeneratedOnAdd(); 

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);

            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);

            builder.HasMany(p => p.Tasks)
                .WithOne(t => t.Patient)
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
