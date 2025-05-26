

using System.ComponentModel.DataAnnotations;

namespace PatientTaskTracker
{
    public class TaskItem
    {

        //TaskID is Primary key
        [Key]
        public int TaskId { get; private set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime Created { get; private set; }
        public bool IsCompleted { get; set; }
        // Foreign key to Patient
        public int PatientId { get; set; }
        public Patient patient { get; set; }

        public TaskItem (int patientId, string description,  DateTime dueDate)
        {
            PatientId = patientId;
            Description = description;
            DueDate = dueDate;
        }

    }
}
