using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker.Core.Managers
{
    public class PatientManagerAsync
    {
        private readonly IPatientRepositoryAsync _patientRepositoryAsync;

        public PatientManagerAsync(IPatientRepositoryAsync patientRepositoryAsync)
        {
            _patientRepositoryAsync = patientRepositoryAsync;
        }


        public bool PatientExists(int patientId)
        {
            return _patientRepositoryAsync.GetPatientByIdAsync(patientId).Result != null;
        }


        public async Task AddPatientAsync(string firstName, string lastName)
        {
            var patient = new Patient(firstName, lastName);
            await _patientRepositoryAsync.AddPatientAsync(patient);
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepositoryAsync.GetAllPatientsAsync();
        }

        public async Task<bool> EditPatientAsync(int patientId, string newFirstName, string newLastName)
        {
            return await _patientRepositoryAsync.UpdatePatientAsync(patientId, newFirstName, newLastName);
        }

        public async Task<bool> RemovePatientAsync(int patientId)
        {
            var patientToRemove = await _patientRepositoryAsync.GetPatientByIdAsync(patientId);
            if (patientToRemove == null)
            {
                return false;
            }

            return await _patientRepositoryAsync.RemovePatientAsync(patientToRemove);
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _patientRepositoryAsync.GetPatientByIdAsync(id);
        }
    }
}
