
namespace PatientTaskTracker
{
    public class DbTaskItemRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

            public DbTaskItemRepository(AppDbContext context)
            {
                _context = context;
            }

        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _context.Tasks.ToList().AsReadOnly();
        }

        public TaskItem GetTaskById(int taskId)
        {
            return _context.Tasks.FirstOrDefault(GetTaskById => GetTaskById.TaskId == taskId);
        }

        public IEnumerable<TaskItem> GetTasksByPatientId(int patientId)
        {
            return _context.Tasks.Where(getTasksByPatientId => getTasksByPatientId.PatientId == patientId).ToList().AsReadOnly();
        }

        public bool RemoveTask(TaskItem task)
        {
            _context.Tasks.Remove(task);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateTask(int taskId, int patientId, string newDescription, DateTime newDueDate)
        {
            var taskToUpdate = GetTaskById(taskId);
            if (taskToUpdate == null)
            {
                return false;
            }

            taskToUpdate.PatientId = patientId;
            taskToUpdate.Description = newDescription;
            taskToUpdate.DueDate = newDueDate;
            return _context.SaveChanges() > 0;

        }
    }
}
