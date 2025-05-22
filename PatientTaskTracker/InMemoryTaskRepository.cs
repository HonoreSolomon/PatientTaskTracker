

namespace PatientTaskTracker
{
    internal class InMemoryTaskRepository : ITaskRepository
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
            if (!TaskExists(task))
            {
                return false;
            }

            _tasks.Remove(task);
            return true;
        }

        public bool UpdateTask(TaskItem updatedTask)
        {
            if (!TaskExists(updatedTask))
            {
                return false;
            }

            var originalTask = GetTaskById(updatedTask.TaskId);

            originalTask.PatientId = updatedTask.PatientId;
            originalTask.Description = updatedTask.Description;
            originalTask.DueDate = updatedTask.DueDate;

            return true;

        }

        public bool MarkTaskAsCompleted(TaskItem taskToComplete)
        {
            if (!TaskExists(taskToComplete))
            {
                return false;
            }

            taskToComplete.IsCompleted = true;
            return true;
        }

    }
}
