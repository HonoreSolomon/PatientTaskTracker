namespace PatientTaskTracker
{
    interface IPatientRepository
    {
        public bool AddPatient(Patient patient);
        public IEnumerable<Patient> GetAllPatients();
        public bool EditPatient(int patientId, string newFirstName, string newLastName);
        public bool RemovePatient(int patientId);
        public bool PatientExists(int patientId);
        public Patient FindPatientById(int patientId);

    }
}
