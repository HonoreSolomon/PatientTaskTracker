using Xunit;
using PatientTaskTracker;
using System.Linq;


namespace PatientTaskTracker.Tests
{
    public class PatientManagerTests
    {
        private PatientManager CreatePatientManagerWithInitialPatient()
        {
            var repo = new InMemoryPatientRepository();
            var manager = new PatientManager(repo);

            manager.AddPatient("John", "Doe");
            return manager;
        }


        [Fact]
        public void PatientExists_ShouldReturnTrueIfPatientExists()
        {
            PatientManager manager = CreatePatientManagerWithInitialPatient();
            manager.AddPatient("Jane", "Smith");
            manager.AddPatient("Bob", "Brown");

            var result = manager.PatientExists(2);

            Assert.True(result);

            
        }

        [Fact]
        public void PatientExists_ShouldReturnFalseIfPatientDoesNotExist()
        {
            var manager = CreatePatientManagerWithInitialPatient();
            manager.AddPatient("Jane", "Smith");
            manager.AddPatient("Bob", "Brown");

            var result = manager.PatientExists(4);
            Assert.False(result);
        }

        [Fact]
        public void AddPatient_ShouldAddPatientToRepository()
        {
            var manager = CreatePatientManagerWithInitialPatient();
            var patients = manager.GetAllPatients();

            Assert.Single(patients);
            var patient = patients.First();
            Assert.Equal("John", patient.FirstName);
            Assert.Equal("Doe", patient.LastName);

            Assert.Equal(1, patient.PatientId);

        }
        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients()
        {
            var manager = CreatePatientManagerWithInitialPatient();
            manager.AddPatient("Jane", "Smith");
            manager.AddPatient("Bob", "Brown");

            var patients = manager.GetAllPatients();

            Assert.Equal(3, patients.Count());
        }

        [Fact]

        public void EditPatient_ShouldUpdatePatientDetails()
        {
            var manager = CreatePatientManagerWithInitialPatient();
            var patient = manager.GetAllPatients().First();

            bool result = manager.EditPatient(patient.PatientId, "Jane", "Smith");
            
            Assert.True(result);
            Assert.Equal("Jane", patient.FirstName);
            Assert.Equal("Smith", patient.LastName);
        }

        

        [Fact]
        public void EditPatient_ShouldReturnFalseIfPatientDoesNotExist()
        {
            var manager = CreatePatientManagerWithInitialPatient();
            var patient = manager.GetAllPatients().First();

            bool result = manager.EditPatient(999, "Jane", "Smith");
            
            Assert.False(result);
        }

        
        [Fact]
        public void RemovePatient_ShouldRemovePatientFromRepository()
        {
            var manager = CreatePatientManagerWithInitialPatient();

            var patient = manager.GetAllPatients().First();
            bool result = manager.RemovePatient(patient.PatientId);
            
            Assert.True(result);
            Assert.Empty(manager.GetAllPatients());



        }
        [Fact]
        public void RemovePatient_ShouldReturnFalseIfPatientDoesNotExist()
        {
            var manager = CreatePatientManagerWithInitialPatient();

            bool result = manager.RemovePatient(999);

            Assert.False(result);
        }
    }
}