
using System.ComponentModel.DataAnnotations;
namespace PatientTaskTracker.Core.Models
{
    public class TaskItem
    {

        [Key]
        public int TaskId { get;  set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get;  set; } 

        public bool IsCompleted { get; set; }

        // Foreign Key
        public Patient Patient { get; set; }
        public int PatientId { get; set; }

        public TaskItem(int patientId, string description, DateTime dueDate)
        {
            PatientId = patientId;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;

        }

        public TaskItem()
        {
            
        }
    }
}
