namespace PatientTaskTracker

{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            PatientManager patientManager = new PatientManager();
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

                        patientManager.AddPatient(firstName, lastName);

                        break;

                    case ("2"):
                        patientManager.ListPatients();
                        foreach (var patient in patientManager)
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case ("3"):
                        Console.WriteLine("Please enter patient ID of patient you would like to edit: ");
                        int patientId;
                        while (!int.TryParse(Console.ReadLine().Trim(), out patientId))
                        {
                             Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }


                        Console.WriteLine("Please enter new first name: ");
                        string newFirstName = Console.ReadLine().Trim();
                        Console.WriteLine("Please enter new last name: ");
                        string newLastName = Console.ReadLine().Trim();

                        if (patientManager.EditPatient(patientId, newFirstName, newLastName))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }



                        Console.WriteLine("Patient info updated.");
                        break;

                    case ("4"):

                        Console.WriteLine("Enter patient ID to be removed: ");
                        int removeId;
                        while (!int.TryParse(Console.ReadLine().Trim(), out removeId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (!patientManager.RemovePatient(removeId))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        }

                        
                        Console.WriteLine("Patient removed.");
                        break;

                    case ("5"):
                        Console.WriteLine("Please enter the patient Id: ");
                        int patientId;
                        while (!int.TryParse(Console.ReadLine().Trim(), out patientId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (!patientManager.Exists(patient => patient.PatientId == patientId))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        int taskId = tasks.Count + 1;

                        Console.WriteLine("Please enter the task description: ");
                        string taskDescription = Console.ReadLine();

                        Console.WriteLine("Please enter the task due date (yyyy-mm-dd): ");
                        DateTime taskDueDate;
                        while (!DateTime.TryParse(Console.ReadLine().Trim(), out taskDueDate))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Due Date: ");
                        }

                        tasks.Add(new Task(patientId, taskDescription, taskDueDate ));
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
                        int taskChoice;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskChoice))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        var taskToEdit = tasks.Find(task => task.TaskId == taskChoice);
                        if (taskToEdit == null)
                        {
                            Console.WriteLine("Task not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                            //throw new ArgumentNullException(nameof(taskToEdit), "Task not found.");
                        }

                        Console.WriteLine("Please enter new patient Id: ");
                        int newPatientId;
                        while (!int.TryParse(Console.ReadLine().Trim(), out newPatientId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (!patientManager.Exists(patient => patient.PatientId == newPatientId))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Please enter new task description: ");
                        string newTaskDescription = Console.ReadLine();

                        Console.WriteLine("Please enter the new task due date (yyyy-mm-dd): ");
                        DateTime newTaskDueDate;
                        while (!DateTime.TryParse(Console.ReadLine().Trim(), out newTaskDueDate))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Due Date: ");
                        }


                        taskToEdit.PatientId = newPatientId;
                        taskToEdit.Description = newTaskDescription;
                        taskToEdit.DueDate = newTaskDueDate;

                        break;

                    case ("8"):
                        Console.WriteLine("Please enter a taskID to delete: ");
                        int taskIdToRemove;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskIdToRemove))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }
                       
                        var taskToRemove = tasks.Find(task => task.TaskId == taskIdToRemove);
                        if (taskToRemove == null)
                        {
                            Console.WriteLine("Task not found.");
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
                        int taskIdToComplete;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskIdToComplete))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        var taskToComplete = tasks.Find(task => task.TaskId == taskIdToComplete);
                        if (taskToComplete == null)
                        {
                            Console.WriteLine("Task not found.");
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