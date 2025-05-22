namespace PatientTaskTracker
{
    interface IPatientRepository
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();
        bool UpdatePatient(Patient patient);
        bool RemovePatient(Patient patient);
        //public bool PatientExists(int patientId);
        Patient GetPatientById(int patientId);

    }
}
