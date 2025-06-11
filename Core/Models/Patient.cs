

using System.ComponentModel.DataAnnotations;

namespace PatientTaskTracker.Core.Models
{
    public class Patient
    {


        [Key]
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();


        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Patient()
        {
            // Parameterless constructor for EF Core
        }








    }
}
