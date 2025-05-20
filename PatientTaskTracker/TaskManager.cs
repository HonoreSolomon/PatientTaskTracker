

namespace PatientTaskTracker
{
    public class TaskManager
    {
        private List<TaskItem> _tasks = new List<TaskItem>();

        private TaskItem FindTaskById(int taskId)
        {
            return _tasks.Find(task => task.TaskId == taskId);
        }

        public void AddTask(int patientId, string description, DateTime DueDate)
        {
            var task = new TaskItem(patientId, description, DueDate);
            _tasks.Add(task);

        }

        public IEnumerable<TaskItem> GetAllTasks()
{
            return _tasks.AsReadOnly();
        }

        
        public bool TaskExists(int taskId)
        {
            return FindTaskById(taskId) != null;
        }

        public bool EditTask(int taskId, int newPatientID, string newDescription, DateTime newDueDate)
        {
            var taskToEdit = FindTaskById(taskId);

            if (taskToEdit == null) {
                return false;
            }

            taskToEdit.PatientId = newPatientID;
            taskToEdit.Description = newDescription;
            taskToEdit.DueDate = newDueDate;

            return true;

        }

        public bool RemoveTask(int taskId)
        {
            var taskToRemove = FindTaskById(taskId);
            if (taskToRemove == null)
            {
                return false;
            }

            _tasks.Remove(taskToRemove);
            return true;
        }

        public bool MarkTaskAsCompleted(int taskId)
        {
            var taskToComplete = FindTaskById(taskId);
            if (taskToComplete == null)
            {
                return false;
            }
            taskToComplete.IsCompleted = true;
            return true;
        }

        public IEnumerable<TaskItem> GetTasksByPatientId(int patientId)
        {
            return _tasks.Where(task => task.PatientId == patientId).ToList();
        }


    }
}
