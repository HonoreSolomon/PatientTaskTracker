

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


        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            PatientId = _nextPatientId++;
        }



        private (string, string) GetPatientInfo()
        {
            Console.WriteLine("Please enter patient first name: ");
            string firstName = Console.ReadLine().Trim();

            Console.WriteLine("Please enter patient last name: ");
            string lastName = Console.ReadLine().Trim();

            return (firstName, lastName);
        }




    }
}
