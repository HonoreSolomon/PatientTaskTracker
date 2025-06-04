using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker.Core.Managers
{
    public class TaskManagerAsync
    {
        private readonly ITaskRepositoryAsync _taskRepositoryAsync;

        public TaskManagerAsync(ITaskRepositoryAsync taskRepositoryAsync)
        {
            _taskRepositoryAsync = taskRepositoryAsync;
        }

        public async Task AddTaskAsync(int patientId, string newDescription, DateTime newDueDate)
        {
            var task = new TaskItem
            {
                PatientId = patientId,
                Description = newDescription,
                DueDate = newDueDate
            };

            await _taskRepositoryAsync.AddTaskAsync(task);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepositoryAsync.GetAllTasksAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
        {
            return await _taskRepositoryAsync.GetTaskByIdAsync(taskId);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByPatientIdAsync(int patientId)
        {
            return await _taskRepositoryAsync.GetTasksByPatientIdAsync(patientId);
        }

        public async Task<bool> UpdateTaskAsync(int taskId, int patientId, string newDescription, DateTime newDueDate)
        {
            return await _taskRepositoryAsync.UpdateTaskAsync(taskId, patientId, newDescription, newDueDate);
        }

        public async Task<bool> RemoveTaskAsync(TaskItem task)
        {
            return await _taskRepositoryAsync.RemoveTaskAsync(task);
        }

        public async Task<bool> TaskExistsAsync(int taskId)
        {
            var task = await _taskRepositoryAsync.GetTaskByIdAsync(taskId);
            return task != null;
        }

        //public async Task<bool> MarkTaskCompleted()
        //{

        //}
    }
}
