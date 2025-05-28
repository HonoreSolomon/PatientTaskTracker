using Microsoft.EntityFrameworkCore;

namespace PatientTaskTracker.Tests
{

    public class DbTaskItemRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;

        public DbTaskItemRepositoryTests()
        {
            var dbPassword = Environment.GetEnvironmentVariable("TEST_DB_PASSWORD") ?? throw new InvalidOperationException("TEST_DB_PASSWORD env variable is not set");
            var connectionString = $"server=localhost;port=3306;database=PatientTaskTrackerTest;user=root;password={dbPassword}";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 4, 0)))
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            // Clean up all data before each test
            _context.Tasks.RemoveRange(_context.Tasks);
            _context.Patients.RemoveRange(_context.Patients);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            // Clean up all data after each test
            _context.Tasks.RemoveRange(_context.Tasks);
            _context.Patients.RemoveRange(_context.Patients);
            _context.SaveChanges();
            _context.Dispose();
        }

        [Fact]
        public void AddTaskItem_ShouldAddTaskItem()
        {
            // Arrange: Create and save a patient
            var patient = new Patient("John", "Doe");
            _context.Patients.Add(patient);
            _context.SaveChanges();

            var taskItemRepository = new DbTaskItemRepository(_context);
            var taskItem = new TaskItem(patient.PatientId, "testTask", DateTime.Now.AddDays(7));

            // Act
            taskItemRepository.AddTask(taskItem);
            var tasks = taskItemRepository.GetAllTasks().ToList();

            // Assert
            Assert.Single(tasks);
            Assert.Equal("testTask", tasks[0].Description);
            Assert.Equal(patient.PatientId, tasks[0].PatientId);
        }

        [Fact]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            var patient = new Patient("Jane", "Smith");
            _context.Patients.Add(patient);
            _context.SaveChanges();

            var taskItemRepository = new DbTaskItemRepository(_context);
            var taskItem1 = new TaskItem(patient.PatientId, "testTask1", DateTime.Now.AddDays(7));
            var taskItem2 = new TaskItem(patient.PatientId, "testTask2", DateTime.Now.AddDays(5));
            taskItemRepository.AddTask(taskItem1);
            taskItemRepository.AddTask(taskItem2);

            var tasks = taskItemRepository.GetAllTasks().ToList();

            Assert.Equal(2, tasks.Count);
            Assert.Contains(tasks, t => t.Description == "testTask1");
            Assert.Contains(tasks, t => t.Description == "testTask2");
        }

        [Fact]
        public void GetTaskById_ShouldReturnCorrectTask()
        {
            var patient = new Patient("Alice", "Brown");
            _context.Patients.Add(patient);
            _context.SaveChanges();

            var taskItemRepository = new DbTaskItemRepository(_context);
            var taskItem = new TaskItem(patient.PatientId, "testTask", DateTime.Now.AddDays(7));
            var taskItem2 = new TaskItem(patient.PatientId, "testTask2", DateTime.Now.AddDays(5));
            taskItemRepository.AddTask(taskItem);
            taskItemRepository.AddTask(taskItem2);

            var retrievedTask = taskItemRepository.GetTaskById(taskItem.TaskId);

            Assert.NotNull(retrievedTask);
            Assert.Equal("testTask", retrievedTask.Description);
            Assert.Equal(patient.PatientId, retrievedTask.PatientId);
        }

        [Fact]
        public void GetTasksByPatientId_ShouldReturnTasksForPatient()
        {
            var patient1 = new Patient("Pat", "One");
            var patient2 = new Patient("Pat", "Two");
            _context.Patients.Add(patient1);
            _context.Patients.Add(patient2);
            _context.SaveChanges();

            var taskItemRepository = new DbTaskItemRepository(_context);
            var taskItem1 = new TaskItem(patient1.PatientId, "testTask1", DateTime.Now.AddDays(7));
            var taskItem2 = new TaskItem(patient1.PatientId, "testTask2", DateTime.Now.AddDays(5));
            var taskItem3 = new TaskItem(patient2.PatientId, "testTask3", DateTime.Now.AddDays(3));
            taskItemRepository.AddTask(taskItem1);
            taskItemRepository.AddTask(taskItem2);
            taskItemRepository.AddTask(taskItem3);

            var tasksForPatient1 = taskItemRepository.GetTasksByPatientId(patient1.PatientId).ToList();
            Assert.Equal(2, tasksForPatient1.Count);
            Assert.All(tasksForPatient1, t => Assert.Equal(patient1.PatientId, t.PatientId));
        }

        [Fact]
        public void RemoveTask_ShouldRemoveTaskFromDatabase()
        {
            var patient = new Patient("John", "Doe");
            _context.Patients.Add(patient);
            _context.SaveChanges();
            var taskItemRepository = new DbTaskItemRepository(_context);
            var taskItem = new TaskItem(patient.PatientId, "testTask", DateTime.Now.AddDays(7));
            taskItemRepository.AddTask(taskItem);

            var removed = taskItemRepository.RemoveTask(taskItem);

            Assert.True(removed);
            var tasks = taskItemRepository.GetAllTasks().ToList();
            Assert.Empty(tasks);
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTaskDetails()
        {
            var patient = new Patient("Jane", "Doe");
            _context.Patients.Add(patient);
            _context.SaveChanges();
            var taskItemRepository = new DbTaskItemRepository(_context);
            var taskItem = new TaskItem(patient.PatientId, "Old Task", DateTime.Now.AddDays(7));
            taskItemRepository.AddTask(taskItem);

            var updated = taskItemRepository.UpdateTask(taskItem.TaskId, patient.PatientId, "Updated Task", DateTime.Now.AddDays(10));
            Assert.True(updated);
            var updatedTask = taskItemRepository.GetTaskById(taskItem.TaskId);
            Assert.Equal("Updated Task", updatedTask.Description);
            Assert.Equal(DateTime.Now.AddDays(10).Date, updatedTask.DueDate.Date);
        }
    }
}
