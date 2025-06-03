using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker.Core.Managers
{
    public class TaskManager
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        //private TaskItem FindTaskById(int taskId)
        //{
        //    return _tasks.Find(task => task.TaskId == taskId);
        //}

        public void AddTask(int patientId, string description, DateTime DueDate)
        {
            //Not checking for duplicates here
            var task = new TaskItem(patientId, description, DueDate);
            _taskRepository.AddTask(task);

        }
        public bool TaskExists(int taskId)
        {
            return _taskRepository.GetTaskById(taskId) != null;
        }

        public IEnumerable<TaskItem> GetAllTasks()
{
            return _taskRepository.GetAllTasks();
        }

        

        public bool EditTask(int taskId, int newPatientID, string newDescription, DateTime newDueDate)
        {
            //var taskToEdit = _taskRepository.GetTaskById(taskId);

            //if (taskToEdit == null) {
            //    return false;
            //}

            
            //taskToEdit.PatientId = newPatientID;
            //taskToEdit.Description = newDescription;
            //taskToEdit.DueDate = newDueDate;




            return _taskRepository.UpdateTask(taskId, newPatientID, newDescription, newDueDate);

        }

        public bool RemoveTask(int taskId)
        {
            var taskToRemove = _taskRepository.GetTaskById(taskId);
            if (taskToRemove == null)
            {
                return false;
            }

            _taskRepository.RemoveTask(taskToRemove);
            return true;
        }

        public bool MarkTaskAsCompleted(int taskId)
        {
            var taskToComplete = _taskRepository.GetTaskById(taskId);
            if (taskToComplete == null)
            {
                return false;
            }
            taskToComplete.IsCompleted = true;
            return true;
        }

        public IEnumerable<TaskItem> GetTasksByPatientId(int patientId)
        {
            return _taskRepository.GetTasksByPatientId(patientId);
        }


    }
}
