using PatientTaskTracker.Infrastructure.Data;
using PatientTaskTracker.Core.Models;
using PatientTaskTracker.Core.Interfaces;   
using Microsoft.EntityFrameworkCore;

namespace PateintTaskTracker.Infrastructure.Repositories
{
    public class DbTaskItemRepository : ITaskRepository, ITaskRepositoryAsync
    {
        private readonly AppDbContext _context;

            public DbTaskItemRepository(AppDbContext context)
            {
                _context = context;
            }

        

        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        

        public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        

        public async Task<IEnumerable<TaskItem>> GetTasksByPatientIdAsync(int patientId)
        {
            return await _context.Tasks.Where (t => t.PatientId == patientId)
                .ToListAsync();
        }


        public async Task<bool> RemoveTaskAsync(TaskItem task)
        {
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);

            return await _context.SaveChangesAsync() > 0;


        }


        public async Task<bool> UpdateTaskAsync(int taskId, int patientId, string newDescription, DateTime newDueDate)
        {
            var taskToUpdate = await GetTaskByIdAsync(taskId);
            if (taskToUpdate == null)
            {
                return false;
            }

            taskToUpdate.PatientId = patientId;
            taskToUpdate.Description = newDescription;
            taskToUpdate.DueDate = newDueDate;

            return await _context.SaveChangesAsync() > 0;

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

        public IEnumerable<TaskItem> GetTasksByPatientId(int patientId)
        {
            return _context.Tasks.Where(getTasksByPatientId => getTasksByPatientId.PatientId == patientId).ToList().AsReadOnly();
        }

        public TaskItem GetTaskById(int taskId)
        {
            return _context.Tasks.FirstOrDefault(GetTaskById => GetTaskById.TaskId == taskId);
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
