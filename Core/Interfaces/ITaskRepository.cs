using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker.Core.Interfaces
{
    public interface ITaskRepository
    {
        
            public void AddTask(TaskItem task);
            public IEnumerable<TaskItem> GetAllTasks();
            public bool UpdateTask(int taskId, int patientId, string newDescription, DateTime newDueDate);
            public bool RemoveTask(TaskItem task);
            //public bool PatientExists(int patientId);
            public TaskItem GetTaskById(int taskId);

            public IEnumerable<TaskItem> GetTasksByPatientId(int patientId);

    }

}


