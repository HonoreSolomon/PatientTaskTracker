using System.ComponentModel.DataAnnotations;

namespace PatientTaskTracker
{
    public class Patient
    {
        //PatientID is Primary key
        [Key]
        public int PatientId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<TaskItem> Tasks { get; set; } = new();

        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

    }

    

}
