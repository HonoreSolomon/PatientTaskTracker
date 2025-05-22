using Xunit;
using PatientTaskTracker;
using System.Linq; 

namespace PatientTaskTracker.Tests
{
    public class InMemoryTaskRepositoryTests
    {
        [Fact]
        public void AddTask_ShouldAddTask()
        {
            var taskRepository = new InMemoryTaskRepository();
            var task = new TaskItem(1, "This is a test task.", DateTime.Now.AddDays(7));

            taskRepository.AddTask(task);
            var tasks = taskRepository.GetAllTasks().ToList();

            Assert.Single(tasks);
            Assert.Equal(task, tasks[0]);
        }

        [Fact]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            var taskRepository = new InMemoryTaskRepository();
            var task1 = new TaskItem(1, "Task 1", DateTime.Now.AddDays(7));
            var task2 = new TaskItem(2, "Task 2", DateTime.Now.AddDays(14));
            taskRepository.AddTask(task1);
            taskRepository.AddTask(task2);

            var tasks = taskRepository.GetAllTasks().ToList();

            Assert.Equal(2, tasks.Count);
            Assert.Contains(task1, tasks);
            Assert.Contains(task2, tasks);
        }

        [Fact]
        public void GetTaskById_ShouldReturnCorrectTask()
        {
            var taskRepository = new InMemoryTaskRepository();
            var task = new TaskItem(1, "Task 1", DateTime.Now.AddDays(7));

            taskRepository.AddTask(task);
            var retrievedTask = taskRepository.GetTaskById(task.TaskId);

            Assert.Equal(task, retrievedTask);
        }

        [Fact]
        public void RemoveTask_ShouldRemoveTask()
        {
            var taskRepository = new InMemoryTaskRepository();
            var task = new TaskItem(1, "Task 1", DateTime.Now.AddDays(7));
            taskRepository.AddTask(task);

            var result = taskRepository.RemoveTask(task);

            Assert.True(result);
            Assert.Empty(taskRepository.GetAllTasks());
        }
    }
}
