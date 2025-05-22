using Xunit;
using PatientTaskTracker;
using System.Linq;

namespace PatientTaskTracker.Tests
{
    public class TaskManagerTests
    {
        TaskManager CreateManagerWithInitialTask()
        {
            var repo = new InMemoryTaskRepository();
            var manager = new TaskManager(repo);

            manager.AddTask(1, "Test Task", DateTime.Now.AddDays(7));
            return manager;
        }

        [Fact]
        public void TaskExists_ShouldReturnTrueIfExists()
        {
            TaskManager manager = CreateManagerWithInitialTask();

            var result = manager.TaskExists(1);

            Assert.True(result);


        }

        [Fact]
        public void TaskExists_ShouldReturnFalseIfDoesNotExist()
        {
            TaskManager manager = CreateManagerWithInitialTask();

            var result = manager.TaskExists(2);

            Assert.False(result);
        }

        [Fact]
        public void AddTask_ShouldAddTaskToRepository()
        {
            TaskManager manager = CreateManagerWithInitialTask();

            var tasks = manager.GetAllTasks();

            Assert.Single(tasks);
        }

        [Fact]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            TaskManager manager = CreateManagerWithInitialTask();
            manager.AddTask(2, "Another Task", DateTime.Now.AddDays(5));
            var tasks = manager.GetAllTasks();
            Assert.Equal(2, tasks.Count());
        }

        [Fact]
        public void GetAllTasks_ShouldReturnEmptyIfNoTasks()
        {
            var repo = new InMemoryTaskRepository();
            var manager = new TaskManager(repo);

            var tasks = manager.GetAllTasks();

            Assert.Empty(tasks);
        }

        [Fact]
        public void GetTasksByPatientId_ShouldReturnTasksForSpecificPatient()
        {
            var manager = CreateManagerWithInitialTask();
            manager.AddTask(2, "Another Task", DateTime.Now.AddDays(5));

            var tasks = manager.GetTasksByPatientId(2);

            Assert.Single(tasks);
        }

        [Fact]

        public void GetTasksByPatientId_ShouldReturnEmptyIfNoTasksForPatient()
        {
            var manager = CreateManagerWithInitialTask();

            var tasks = manager.GetTasksByPatientId(3);

            Assert.Empty(tasks);
        }




        [Fact]
        public void EditTask_ShouldUpdateTaskDetails()
        {
            var manager = CreateManagerWithInitialTask();
            var task = manager.GetAllTasks().First();

            var result = manager.EditTask(task.TaskId, 2, "BloodWork", DateTime.Now.AddDays(3));

            Assert.True(result);
            Assert.Equal(2, task.PatientId);
            Assert.Equal("BloodWork", task.Description);
            Assert.Equal(DateTime.Now.AddDays(3).Date, task.DueDate.Date);


        }

        [Fact]
        public void EditTask_ShouldReturnFalseIfTaskDoesNotExist()
        {
            var manager = CreateManagerWithInitialTask();
            var result = manager.EditTask(2, 2, "BloodWork", DateTime.Now.AddDays(3));
            Assert.False(result);
        }

        [Fact]
        public void RemoveTask_ShouldRemoveTask()
        {
            var manager = CreateManagerWithInitialTask();
            var task = manager.GetAllTasks().First();

            var result = manager.RemoveTask(task.TaskId);

            Assert.True(result);
            Assert.Empty(manager.GetAllTasks());
        }

        [Fact]
        public void RemoveTask_ShouldReturnFalseIfTaskDoesNotExist()
        {
            var manager = CreateManagerWithInitialTask();

            var result = manager.RemoveTask(2);

            Assert.False(result);
        }

        [Fact]
        public void MarkTaskAsCompleted_ShouldMarkTaskAsComplete()
        {
            var manager = CreateManagerWithInitialTask();
            var task = manager.GetAllTasks().First();

            var result = manager.MarkTaskAsCompleted(task.TaskId);

            Assert.True(result);
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public void MarkTaskAsCompleted_ShouldReturnFalseIfTaskDoesNotExist()
        {
            var manager = CreateManagerWithInitialTask();

            var result = manager.MarkTaskAsCompleted(2);

            Assert.False(result);
        }


    }
}
