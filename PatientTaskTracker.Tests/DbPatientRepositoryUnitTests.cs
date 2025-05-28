using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using PatientTaskTracker;

namespace PatientTaskTracker.Tests
{
    public class DbPatientRepositoryUnitTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public void AddPatient_AddsPatientToDatabase()
        {
            var context = GetInMemoryDbContext();
            var repo = new DbPatientRepository(context);
            var patient = new Patient("John", "Doe");

            repo.AddPatient(patient);

            Assert.Single(context.Patients);
            Assert.Equal("John", context.Patients.First().FirstName);
        }

        [Fact]
        public void GetAllPatients_ReturnsAllPatients()
        {
            var context = GetInMemoryDbContext();
            context.Patients.Add(new Patient("Jane", "Smith"));
            context.Patients.Add(new Patient("Bob", "Brown"));
            context.SaveChanges();

            var repo = new DbPatientRepository(context);
            var patients = repo.GetAllPatients().ToList();

            Assert.Equal(2, patients.Count);
        }

        [Fact]
        public void GetPatientById_ReturnsCorrectPatient()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Alice", "White");
            context.Patients.Add(patient);
            context.SaveChanges();

            var repo = new DbPatientRepository(context);
            var result = repo.GetPatientById(patient.PatientId);

            Assert.NotNull(result);
            Assert.Equal("Alice", result.FirstName);
        }

        [Fact]
        public void RemovePatient_RemovesPatientFromDatabase()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Eve", "Black");
            context.Patients.Add(patient);
            context.SaveChanges();

            var repo = new DbPatientRepository(context);
            var removed = repo.RemovePatient(patient);

            Assert.True(removed);
            Assert.Empty(context.Patients);
        }

        [Fact]
        public void UpdatePatient_UpdatesPatientDetails()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient("Old", "Name");
            context.Patients.Add(patient);
            context.SaveChanges();

            var repo = new DbPatientRepository(context);
            var updated = repo.UpdatePatient(patient.PatientId, "New", "Name");

            Assert.True(updated);
            var updatedPatient = context.Patients.First();
            Assert.Equal("New", updatedPatient.FirstName);
        }
    }
}