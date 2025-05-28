using Microsoft.EntityFrameworkCore;
namespace PatientTaskTracker.Tests
{
    public class DbPatientRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;

        public DbPatientRepositoryTests()
        {
            var dbPassword = Environment.GetEnvironmentVariable("TEST_DB_PASSWORD") ?? throw new InvalidOperationException("TEST_DB_PASSWORD env variable is not set");
            var connectionString = $"server=localhost;port=3306;database=PatientTaskTrackerTest;user=root;password={dbPassword}";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 4, 0)))
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            // Clean up all data before each test
            _context.Tasks.RemoveRange(_context.Tasks);
            _context.Patients.RemoveRange(_context.Patients);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            // Clean up all data after each test
            _context.Tasks.RemoveRange(_context.Tasks);
            _context.Patients.RemoveRange(_context.Patients);
            _context.SaveChanges();
            _context.Dispose();
        }


        [Fact]
        public void AddPatient_ShouldAddPatient()
        {
            var patientRepository = new DbPatientRepository(_context);
            var patient = new Patient("John", "Doe");

            patientRepository.AddPatient(patient);
            var patients = patientRepository.GetAllPatients().ToList();
            var addedPatient = patients[0];

            Assert.Single(patients);
            Assert.Equal(patient.FirstName, addedPatient.FirstName);
            Assert.Equal(patient.LastName, addedPatient.LastName);
        }

        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients()
        {
            var patientRepository = new DbPatientRepository(_context);
            var patient1 = new Patient("John", "Doe");
            var patient2 = new Patient("Jane", "Smith");
            patientRepository.AddPatient(patient1);
            patientRepository.AddPatient(patient2);

            var patients = patientRepository.GetAllPatients().ToList();

            Assert.Equal(2, patients.Count);
            Assert.Contains(patient1, patients);
            Assert.Contains(patient2, patients);
        }

        [Fact]

        public void GetPatientById_ShouldReturnCorrectPatient()
        {
            var patientRepository = new DbPatientRepository(_context);
            var patient = new Patient("John", "Doe");
            var patient2 = new Patient("Jane", "Smith");
            var patient3 = new Patient("Bob", "Brown");
            patientRepository.AddPatient(patient);
            patientRepository.AddPatient(patient2);
            patientRepository.AddPatient(patient3);

            var patientId = patient.PatientId;
            var patientToGet = patientRepository.GetPatientById(patientId);

            Assert.Equal(patient, patientToGet);
        }

        [Fact]
        public void RemovePatient_ShouldRemovePatient()
        {
            var patientRepository = new DbPatientRepository(_context);
            var patient = new Patient("John", "Doe");
            patientRepository.AddPatient(patient);

            var result = patientRepository.RemovePatient(patient);

            Assert.True(result);
            Assert.Empty(patientRepository.GetAllPatients());
        }

        [Fact]
        public void UpdatePatient_ShouldUpdatePatient()
        {
            var patientRepository = new DbPatientRepository(_context);
            var patient = new Patient("John", "Doe");
            patientRepository.AddPatient(patient);


        }
    }
}
