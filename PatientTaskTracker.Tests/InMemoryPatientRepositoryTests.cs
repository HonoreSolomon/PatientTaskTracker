using Xunit;
using PatientTaskTracker;
using System.Linq;

namespace PatientTaskTracker.Tests
{
    public class InMemoryPatientRepositoryTests
    {
        [Fact]
        public void AddPatient_ShouldAddPatient()
        {

            var patientRepository = new InMemoryPatientRepository();
            var patient = new Patient("John", "Doe");

            patientRepository.AddPatient(patient);

            var patients = patientRepository.GetAllPatients().ToList();

            Assert.Single(patients);
            Assert.Equal(patient, patients[0]);
        }

        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients()
        {
            var patientRepository = new InMemoryPatientRepository();
            var patient1 = new Patient("John", "Doe");
            var patient2 = new Patient("Jane", "Smith");

            patientRepository.AddPatient(patient1);
            patientRepository.AddPatient(patient2);
            var patients = patientRepository.GetAllPatients().ToList();

            Assert.Equal(2, patients.Count);
            Assert.Contains(patient1, patients);
            Assert.Contains(patient2, patients);
        }

        [Fact]
        public void GetPatientById_ShouldReturnCorrectPatient()
        {
            var patientRepository = new InMemoryPatientRepository();
            var patient = new Patient("John", "Doe");
            patientRepository.AddPatient(patient);

            var retrievedPatient = patientRepository.GetPatientById(patient.PatientId);

            Assert.Equal(patient, retrievedPatient);
        }


        [Fact]
        public void RemovePatient_ShouldRemovePatient()
        {
            var patientRepository = new InMemoryPatientRepository();
            var patient = new Patient("John", "Doe");
            patientRepository.AddPatient(patient);

            var result = patientRepository.RemovePatient(patient);

            Assert.True(result);
            Assert.Empty(patientRepository.GetAllPatients());
        }
    }
}
