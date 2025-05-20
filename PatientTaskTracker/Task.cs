

namespace PatientTaskTracker
{
    public class Task
    {
        private static int _nextTaskId = 1;
        private int _taskId;
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public int TaskId         {
            get { return _taskId; }
            private set { _taskId = value; }
        }
        public DateTime Created { get; private set; }

        public bool IsCompleted { get; set; } = false;

        public Task(int patientID, string description, DateTime DueDate)
        {
            Created = DateTime.Now;
            TaskId = _nextTaskId++;
            IsCompleted = false;
        }
    }
}
