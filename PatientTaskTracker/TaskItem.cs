

namespace PatientTaskTracker
{
    public class TaskItem
    {
        private static int _nextTaskId = 1;
        private int _taskId;
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public int TaskId
        {
            get { return _taskId; }
            private set { _taskId = value; }
        }
        public DateTime Created { get; private set; }

        public bool IsCompleted { get; set; } = false;
        public Patient Patient { get; set; }

        public TaskItem(int patientId, string description, DateTime dueDate)
        {
            PatientId = patientId;
            Description = description;
            DueDate = dueDate;

            Created = DateTime.Now;
            TaskId = _nextTaskId++;
            IsCompleted = false;
        }

        public TaskItem()
        {
            
        }
    }
}
