
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
            throw new NotImplementedException();
        }

        public TaskItem GetTaskById(int taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskItem> GetTasksByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTask(TaskItem task)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTask(TaskItem task)
        {
            throw new NotImplementedException();
        }
    }
}
