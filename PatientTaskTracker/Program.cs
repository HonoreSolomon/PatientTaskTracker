namespace PatientTaskTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Patient> patients = new List<Patient>();
            List<Task> tasks = new List<Task>();
            
            Console.WriteLine("Patient Task Tracker Has been booted. ");
            while (true)
            {

                
                Console.WriteLine("Please choose on of the following options. \n");
                Console.WriteLine("1. Add a new patient. ");
                Console.WriteLine("2. List Current patients.");
                Console.WriteLine("3. Edit patient info. ");
                Console.WriteLine("4. Remove a patient. ");
                Console.WriteLine("5. Add a new task. ");
                Console.WriteLine("6. List current tasks. ");
                Console.WriteLine("7. Edit a task. ");
                Console.WriteLine("8. Remove a task ");
                Console.WriteLine("9. Mark task complete. ");
                Console.WriteLine("10. Exit.\n");
                var input = Console.ReadLine().Trim();
                switch (input)
                {
                    case ("1"):
                        Console.WriteLine("Please enter patient first name: ");
                        string firstName = Console.ReadLine().Trim();

                        Console.WriteLine("Please enter patient last name: ");
                        string lastName = Console.ReadLine().Trim();
            
                        patients.Add(new Patient{ FirstName = firstName, LastName = lastName, PatientId = patients.Count + 1 });
                        Console.WriteLine("Patient Added.");
                        break;

                    case ("2"):
                        foreach (var patient in patients)
                        {
                            Console.WriteLine($"Patient ID: {patient.PatientId}, Name: {patient.FirstName} {patient.LastName}");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case ("3"):
                        Console.WriteLine("Please enter patient ID of patient you would like to edit: ");
                        int choice = int.Parse(Console.ReadLine().Trim());
                        var patientToEdit = patients.Find(patient => patient.PatientId == choice);

                        if (patientToEdit == null)
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                            //throw new ArgumentNullException(nameof(patientToEdit), "Patient not found");
                        }

                        Console.WriteLine("Please enter new first name: ");
                        string newFirstName = Console.ReadLine().Trim();
                        Console.WriteLine("Please enter new last name: ");
                        string newLastName = Console.ReadLine().Trim();
                        
                        patientToEdit.FirstName = newFirstName;
                        patientToEdit.LastName = newLastName;

                        Console.WriteLine("Patient info updated.");
                        break;

                    case ("4"):
                        Console.WriteLine("Enter patient ID to be removed: ");
                        int removeId = int.Parse(Console.ReadLine().Trim());
                        var patientToRemove = patients.Find(patient => patient.PatientId == removeId);
                        if (patientToRemove == null)
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                            //throw new ArgumentNullException(nameof(patientToRemove), "Patient not found.");
                        }
                        
                        patients.Remove(patientToRemove);
                        Console.WriteLine("Patient removed.");
                        break;

                    case ("5"):
                        Console.WriteLine("Please enter the patient Id: ");                     
                        int patientId = int.Parse(Console.ReadLine().Trim());

                        int taskId = tasks.Count + 1;

                        Console.WriteLine("Please enter the task description: ");
                        string taskDescription = Console.ReadLine();

                        Console.WriteLine("Please enter the task due date (yyyy-mm-dd): ");
                        DateTime taskDueDate = DateTime.Parse(Console.ReadLine().Trim());

                        tasks.Add(new Task { PatientId = patientId, TaskId = taskId, Description = taskDescription, DueDate = taskDueDate });
                        Console.WriteLine("Task added.");

                        break;
                        

                    case ("6"):
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"TaskId: {task.TaskId}, PatientID: {task.PatientId}, Task Description: {task.Description}, Due Date: {task.DueDate}, Created On: {task.Created} Completed: {task.IsCompleted}");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case ("7"):
                        Console.WriteLine("Please enter task ID to edit: ");
                        int taskChoice = int.Parse(Console.ReadLine().Trim());

                        var taskToEdit = tasks.Find(task => task.TaskId == taskChoice);
                        if (taskToEdit == null)
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                            //throw new ArgumentNullException(nameof(taskToEdit), "Task not found.");
                        }

                        Console.WriteLine("Please enter new patient Id: ");
                        int newPatientId = int.Parse(Console.ReadLine().Trim());

                        Console.WriteLine("Please enter new task description: ");
                        string newTaskDescription = Console.ReadLine();

                        Console.WriteLine("Please enter the new task due date (yyyy-mm-dd): ");
                        DateTime newTaskDueDate = DateTime.Parse(Console.ReadLine().Trim());

                        taskToEdit.PatientId = newPatientId;
                        taskToEdit.Description = newTaskDescription;
                        taskToEdit.DueDate = newTaskDueDate;

                        break;

                    case ("8"):
                        Console.WriteLine("Please enter a taskID to delete: ");
                        int taskIdToRemove = int.Parse(Console.ReadLine().Trim());

                        var taskToRemove = tasks.Find(task => task.TaskId == taskIdToRemove);
                        if (taskToRemove == null)
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                            //throw new ArgumentNullException(nameof(taskToRemove), "Task not found.");
                        }

                        tasks.Remove(taskToRemove);
                        Console.WriteLine("Task removed.");

                        break;

                    case ("9"):
                        Console.WriteLine("Please enter taskID to complete: ");
                        int taskIdToComplete = int.Parse(Console.ReadLine().Trim());

                        var taskToComplete = tasks.Find(task => task.TaskId == taskIdToComplete);
                        if (taskToComplete == null)
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                            //throw new ArgumentNullException(nameof(taskToComplete), "Task not found.");
                        }

                        taskToComplete.IsCompleted = true;

                        Console.WriteLine("Task marked as complete.");

                        break;

                    case ("10"):
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }
        }
    }
}