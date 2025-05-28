

namespace PatientTaskTracker
{
    public class DbPatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public DbPatientRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList().AsReadOnly();
        }

        public Patient GetPatientById(int patientId)
        {
            return _context.Patients.FirstOrDefault(GetPatientById => GetPatientById.PatientId == patientId);
        }

        public bool RemovePatient(Patient patient)
        {
            _context.Patients.Remove(patient);
            return _context.SaveChanges() > 0; 
        }

        public bool UpdatePatient(int patientId, string newFirstName, string newLastName )
        {
            var patientToUpdate = GetPatientById(patientId);
            if (patientToUpdate == null)
            {
                return false;
            }

            patientToUpdate.FirstName = newFirstName;
            patientToUpdate.LastName = newLastName;

            
            return _context.SaveChanges() > 0;
        }
    }
}
