using PatientTaskTracker.Core.Models;

namespace PatientTaskTracker.Core.Interfaces
{
    public interface IPatientRepositoryAsync
    {
        Task AddPatientAsync(Patient patient);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int patientId);
        Task<bool> UpdatePatientAsync(int patientId, string newFirstName, string newLastName);
        Task<bool> RemovePatientAsync(Patient patient);
    }
}
