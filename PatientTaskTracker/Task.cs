

namespace PatientTaskTracker
{
    public class Task
    {
        public int TaskId { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get; private set; }

        public bool IsCompleted { get; set; } = false;

        public Task()
        {
            Created = DateTime.Now;
        }
    }
}
