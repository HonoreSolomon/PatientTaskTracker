
namespace PatientTaskTracker
{
    public class PatientManager
    {
        //private List<Patient> _patients = new List<Patient>();
        private readonly IPatientRepository _patientRepository;


        public PatientManager(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public bool PatientExists(int patientId)
        {
            if(_patientRepository.GetPatientById(patientId) == null)
            {
                return false;
            }
            
            return true;

        }



        public void AddPatient(string firstName, string lastName)
        {
            var patient = new Patient(firstName, lastName);

            //currently not checking for duplicates

            _patientRepository.AddPatient(patient);

        }
        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAllPatients();
        }


        public bool EditPatient(int patientId, string newFirstName, string newLastName)
        {
            var patientToEdit = _patientRepository.GetPatientById(patientId);

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
            var patientToRemove = _patientRepository.GetPatientById(patientId);
            if (patientToRemove == null)
            {
                return false;
            }

            _patientRepository.RemovePatient(patientToRemove);

            return true;
        }

    }
}
