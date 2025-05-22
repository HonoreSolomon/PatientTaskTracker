namespace PatientTaskTracker
{
    public interface IPatientRepository
    {
        public void AddPatient(Patient patient);
        public IEnumerable<Patient> GetAllPatients();
        public bool UpdatePatient(Patient patient);
        public bool RemovePatient(Patient patient);
        //public bool PatientExists(int patientId);
        public Patient GetPatientById(int patientId);

    }
}
