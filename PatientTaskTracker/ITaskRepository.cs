namespace PatientTaskTracker
{
    interface ITaskRepository
    {
        
            void AddTask(TaskItem task);
            IEnumerable<TaskItem> GetAllTasks();
            bool UpdateTask(TaskItem task);
            bool RemoveTask(TaskItem task);
            //public bool PatientExists(int patientId);
            TaskItem GetTaskById(int taskId);

            IEnumerable<TaskItem> GetTasksByPatientId(int patientId);

    }

}


