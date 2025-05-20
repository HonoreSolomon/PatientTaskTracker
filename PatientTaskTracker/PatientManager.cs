
namespace PatientTaskTracker
{
    public class PatientManager
    {
        private List<Patient> _patients = new List<Patient>();

        private Patient FindPatientById(int patientId)



        {
            return _patients.Find(patient => patient.PatientId == patientId);
        }

        public bool PatientExists(int patientId)
        {
            return FindPatientById(patientId) != null;
        }


        public void AddPatient(string firstName, string lastName)
        {
            var patient = new Patient(firstName, lastName);
            _patients.Add(patient);
            Console.WriteLine($"Patient {firstName} {lastName} added with ID {patient.PatientId}.");
        }
        public IEnumerable<Patient> GetAllPatients()
        {
            return _patients.AsReadOnly();
        }


        public bool EditPatient(int patientId, string newFirstName, string newLastName)
        {
            var patientToEdit = FindPatientById(patientId);

            if (patientToEdit == null)
            {
                return false;
            }

            patientToEdit.FirstName = newFirstName;
            patientToEdit.LastName = newLastName;

            return true;
        }

        public bool RemovePatient(int patientId)
        {
            var patientToRemove = FindPatientById(patientId);
            if (patientToRemove == null)
            {
                return false;
            }

            _patients.Remove(patientToRemove);

            return true;
        }
    }
}
