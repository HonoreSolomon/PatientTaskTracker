namespace PatientTaskTracker
{
    public class InMemoryPatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients = new List<Patient>();

        private bool PatientExists(Patient patient)
        {
            return _patients.Exists(p => p.PatientId == patient.PatientId);
        }
        
        public void AddPatient(Patient patient)
        {             
            _patients.Add(patient);

        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patients.AsReadOnly();
        }

        public Patient GetPatientById(int patientId)
        {
            // Checking if patient exists in the PatientManager class
            return _patients.Find(patient => patient.PatientId == patientId);
        }

        public bool UpdatePatient(Patient updatedPatient)
        {
            if (!PatientExists(updatedPatient))
            {
                return false;
            }
            
            var existingPatient = GetPatientById(updatedPatient.PatientId);

            existingPatient.FirstName = updatedPatient.FirstName;
            existingPatient.LastName = updatedPatient.LastName;
            return true;

        }
        public bool RemovePatient(Patient patient)
        {
            if (!PatientExists(patient))
            {
                return false;
            }
            _patients.Remove(patient);
            return true;
        }

    }
}
