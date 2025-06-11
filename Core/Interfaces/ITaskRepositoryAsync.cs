using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker.Core.Interfaces
{
    public interface ITaskRepositoryAsync
    {

        Task AddTaskAsync(TaskItem task);

        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<bool> UpdateTaskAsync(int taskId, int patientId, string newDescription, DateTime newDueDate);
        Task<bool> RemoveTaskAsync(TaskItem task);

        Task<TaskItem?> GetTaskByIdAsync(int taskId);

        Task<IEnumerable<TaskItem>> GetTasksByPatientIdAsync(int patientId);





    }

}