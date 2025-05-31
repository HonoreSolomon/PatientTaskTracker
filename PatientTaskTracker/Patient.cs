

namespace PatientTaskTracker
{
    public class Patient
    {

        private static int _nextPatientId = 1;

        private int _patientId;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int PatientId
        {
            get { return _patientId; }
            private set { _patientId = value; }

        }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();


        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            PatientId = _nextPatientId++;
        }

        public Patient()
        {
            // Parameterless constructor for EF Core
        }








    }
}
