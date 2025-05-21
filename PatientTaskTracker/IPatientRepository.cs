namespace PatientTaskTracker
{
    interface IPatientRepository
    {
        public bool Add(Patient patient);
        public IEnumerable<Patient> GetAllPatients();
        public bool Update(int patientId, string newFirstName, string newLastName);
        public bool Remove(int patientId);
        public bool PatientExists(int patientId);
        public Patient GetById(int patientId);

    }
}
