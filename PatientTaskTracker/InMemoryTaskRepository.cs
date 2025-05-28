

namespace PatientTaskTracker
{
    public class InMemoryTaskRepository : ITaskRepository
    {

        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        private bool TaskExists(TaskItem task)
        {
            return _tasks.Exists(taskItem => taskItem.TaskId == task.TaskId);
        }
        public void AddTask(TaskItem task)
        {
            _tasks.Add(task);
        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _tasks.AsReadOnly();
        }

        public TaskItem GetTaskById(int taskId)
        {
            return _tasks.Find(task => task.TaskId == taskId);
        }

        public IEnumerable<TaskItem> GetTasksByPatientId(int patientId)
        {
            return _tasks.FindAll(task => task.PatientId == patientId).AsReadOnly();
        }

        public bool RemoveTask(TaskItem task)
        {
            if (task == null)
            {
                return false;
            }

            _tasks.Remove(task);
            return true;
        }

        public bool UpdateTask(int taskId, int patientId, string description, DateTime dueDate)
        {
            var updatedTask = GetTaskById(taskId);
            if (updatedTask == null)
            {
                return false;
            }


            updatedTask.PatientId = patientId;
            updatedTask.Description = description;
            updatedTask.DueDate = dueDate;

            return true;

        }

        public bool MarkTaskAsCompleted(int taskId)
        {
            var taskToComplete = GetTaskById(taskId);
            if (taskToComplete == null)
            {
                return false;
            }

            taskToComplete.IsCompleted = true;
            return true;
        }

    }
}
