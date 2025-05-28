namespace PatientTaskTracker

{

    public class Program
    {
        public static void Main(string[] args)
        {
            InMemoryPatientRepository inMemoryPatientRepository = new();
            InMemoryTaskRepository inMemoryTaskRepository = new();
            PatientManager patientManager = new(inMemoryPatientRepository);
            TaskManager taskManager = new(inMemoryTaskRepository);

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
                Console.WriteLine("7  List tasks for patient ");
                Console.WriteLine("8. Edit a task. ");
                Console.WriteLine("9. Remove a task ");
                Console.WriteLine("10. Mark task complete. ");
                Console.WriteLine("11. Exit.\n");
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
                        var patientList = patientManager.GetAllPatients();
                        foreach (var patient in patientList)
                        {
                            Console.WriteLine($"Patient ID: {patient.PatientId}, Name: {patient.FirstName} {patient.LastName}");
                        }

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

                        if (!patientManager.PatientExists(patientId))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Please enter new first name: ");
                        string newFirstName = Console.ReadLine().Trim();
                        Console.WriteLine("Please enter new last name: ");
                        string newLastName = Console.ReadLine().Trim();



                        patientManager.EditPatient(patientId, newFirstName, newLastName);
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
                        int taskPatientId;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskPatientId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (!patientManager.PatientExists(taskPatientId))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }


                        Console.WriteLine("Please enter the task description: ");
                        string taskDescription = Console.ReadLine();

                        Console.WriteLine("Please enter the task due date (yyyy-mm-dd): ");
                        DateTime taskDueDate;
                        while (!DateTime.TryParse(Console.ReadLine().Trim(), out taskDueDate))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Due Date: ");
                        }


                        taskManager.AddTask(taskPatientId, taskDescription, taskDueDate);
                        Console.WriteLine("Task added.");

                        break;


                    case ("6"):

                        var tasks = taskManager.GetAllTasks();
                        if (tasks.Count() == 0)
                        {
                            Console.WriteLine("No tasks found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"TaskId: {task.TaskId}, PatientID: {task.PatientId}, Task Description: {task.Description}, Due Date: {task.DueDate}, Created On: {task.Created} Completed: {task.IsCompleted}");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case ("7"):

                        Console.WriteLine("Please enter the patient Id: ");
                        int patientIdForTasks;

                        while (!int.TryParse(Console.ReadLine().Trim(), out patientIdForTasks))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (!patientManager.PatientExists(patientIdForTasks))
                        {
                            Console.WriteLine("Patient not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }


                        var tasksForPatient = taskManager.GetTasksByPatientId(patientIdForTasks);
                        foreach (var task in tasksForPatient)
                        {
                            Console.WriteLine($"TaskId: {task.TaskId}, PatientID: {task.PatientId}, Task Description: {task.Description}, Due Date: {task.DueDate}, Created On: {task.Created} Completed: {task.IsCompleted}");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;


                    case ("8"):
                        Console.WriteLine("Please enter task ID to edit: ");
                        int taskChoice;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskChoice))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        Console.WriteLine("Please enter new patient Id: ");
                        int newPatientId;
                        while (!int.TryParse(Console.ReadLine().Trim(), out newPatientId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (!patientManager.PatientExists(newPatientId))
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

                        if (taskManager.EditTask(taskChoice, newPatientId, newTaskDescription, newTaskDueDate))
                        {
                            Console.WriteLine("Task updated.");
                        }

                        else
                        {
                            Console.WriteLine("Task not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        break;

                    case ("9"):
                        Console.WriteLine("Please enter a taskID to delete: ");
                        int taskIdToRemove;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskIdToRemove))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }


                        if (taskManager.RemoveTask(taskIdToRemove))
                        {
                            Console.WriteLine("Task removed.");
                        }
                        else
                        {
                            Console.WriteLine("Task not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        break;

                    case ("10"):
                        Console.WriteLine("Please enter taskID to complete: ");
                        int taskIdToComplete;
                        while (!int.TryParse(Console.ReadLine().Trim(), out taskIdToComplete))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID: ");
                        }

                        if (taskManager.MarkTaskAsCompleted(taskIdToComplete))
                        {
                            Console.WriteLine("Task marked as complete.");
                        }
                        else
                        {
                            Console.WriteLine("Task not found.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        break;

                    case ("11"):
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