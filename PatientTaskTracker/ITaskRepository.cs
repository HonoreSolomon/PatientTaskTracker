namespace PatientTaskTracker
{
    public interface ITaskRepository
    {
        
            public void AddTask(TaskItem task);
            public IEnumerable<TaskItem> GetAllTasks();
            public bool UpdateTask(TaskItem task);
            public bool RemoveTask(TaskItem task);
            //public bool PatientExists(int patientId);
            public TaskItem GetTaskById(int taskId);

            public IEnumerable<TaskItem> GetTasksByPatientId(int patientId);

    }

}


