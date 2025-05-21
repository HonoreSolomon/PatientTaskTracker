namespace PatientTaskTracker
{
    public class InMemoryPatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients = new List<Patient>();

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
            return _patients.Find(patient => patient.PatientId == patientId);
        }
        public bool UpdatePatient(Patient updatedPatient)
        {
            Patient? existingPatient;
            (bool flowControl, bool value) = patientExists(updatedPatient, out existingPatient);
            if (!flowControl)
            {
                return value;
            }

            existingPatient.FirstName = updatedPatient.FirstName;
            existingPatient.LastName = updatedPatient.LastName;
            return true;

        }

        private (bool flowControl, bool value) patientExists(Patient updatedPatient, out Patient? existingPatient)
        {
            existingPatient = _patients.Find(patient => patient.PatientId == updatedPatient.PatientId);
            if (existingPatient == null)
            {
                return (flowControl: false, value: false);
            }

            return (flowControl: true, value: default);
        }

        public bool RemovePatient(Patient patient)
        {
            
            _patients.Remove(patient);
        }

    }
}
