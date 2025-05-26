using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace PatientTaskTracker
{
    public class PatientTaskTrackerDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        public PatientTaskTrackerDbContext(DbContextOptions<PatientTaskTrackerDbContext>) : base(options)
        {
            // Database initialization logic can go here, if needed
        }

    }
}