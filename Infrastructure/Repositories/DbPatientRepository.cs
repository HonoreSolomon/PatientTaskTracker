using PatientTaskTracker.Infrastructure.Data;
using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PatientTaskTracker.Infrastructure.Repositories
{
    public class DbPatientRepository : IPatientRepository, IPatientRepositoryAsync
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


        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.Tasks)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            return await _context.Patients
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        public async Task<bool> UpdatePatientAsync(int patientId, string newFirstName, string newLastName)
        {
            var patientToUpdate = await GetPatientByIdAsync(patientId);
            if (patientToUpdate == null)
            {
                return false;
            }

            patientToUpdate.FirstName = newFirstName;
            patientToUpdate.LastName = newLastName;

            return await _context.SaveChangesAsync() > 0;



        }

        public async Task<bool> RemovePatientAsync(Patient patient)
        {
            if (!await _context.Patients.AnyAsync(p => p.PatientId == patient.PatientId))
            {
                return false;
            }

            _context.Patients.Remove(patient);

            

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
