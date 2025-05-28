using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using PatientTaskTracker;

namespace PatientTaskTracker.Tests
{
    public class DbTaskItemRepositoryUnitTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new AppDbContext(options);
            return context;
        }

        [Fact]
        public void AddTask_AddsTaskToDatabase()
        {
            var context = GetInMemoryDbContext();
            // Create and save a patient first
            var patient = new Patient("John", "Doe");
            context.Patients.Add(patient);
            context.SaveChanges();

            var repo = new DbTaskItemRepository(context);
            var task = new TaskItem(patient.PatientId, "Test Task", DateTime.Now.AddDays(1));

            repo.AddTask(task);

            Assert.Single(context.Tasks);
            Assert.Equal("Test Task", context.Tasks.First().Description);
            Assert.Equal(patient.PatientId, context.Tasks.First().PatientId);
        }

        [Fact]
        public void GetAllTasks_ReturnsAllTasks()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Jane", "Smith");
            context.Patients.Add(patient);
            context.SaveChanges();

            context.Tasks.Add(new TaskItem(patient.PatientId, "Task 1", DateTime.Now.AddDays(1)));
            context.Tasks.Add(new TaskItem(patient.PatientId, "Task 2", DateTime.Now.AddDays(2)));
            context.SaveChanges();

            var repo = new DbTaskItemRepository(context);

            var tasks = repo.GetAllTasks().ToList();

            Assert.Equal(2, tasks.Count);
        }

        [Fact]
        public void GetTaskById_ReturnsCorrectTask()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Alice", "Brown");
            context.Patients.Add(patient);
            context.SaveChanges();

            var task = new TaskItem(patient.PatientId, "Task 1", DateTime.Now.AddDays(1));
            context.Tasks.Add(task);
            context.SaveChanges();

            var repo = new DbTaskItemRepository(context);

            var result = repo.GetTaskById(task.TaskId);

            Assert.NotNull(result);
            Assert.Equal("Task 1", result.Description);
            Assert.Equal(patient.PatientId, result.PatientId);
        }

        [Fact]
        public void RemoveTask_RemovesTaskFromDatabase()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Bob", "White");
            context.Patients.Add(patient);
            context.SaveChanges();

            var task = new TaskItem(patient.PatientId, "Task 1", DateTime.Now.AddDays(1));
            context.Tasks.Add(task);
            context.SaveChanges();

            var repo = new DbTaskItemRepository(context);

            var removed = repo.RemoveTask(task);

            Assert.True(removed);
            Assert.Empty(context.Tasks);
        }

        [Fact]
        public void UpdateTask_UpdatesTaskInDatabase()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Eve", "Black");
            context.Patients.Add(patient);
            context.SaveChanges();

            var repo = new DbTaskItemRepository(context);
            var task = new TaskItem(patient.PatientId, "Old Description", DateTime.Now.AddDays(1));
            repo.AddTask(task);

            var taskId = task.TaskId;



            var updated = repo.UpdateTask(task.TaskId, patient.PatientId, "New Description", DateTime.Now.AddDays(2) );

            Assert.True(updated);
            var updatedTask = context.Tasks.First();
            Assert.Equal("New Description", updatedTask.Description);
        }
    }
}