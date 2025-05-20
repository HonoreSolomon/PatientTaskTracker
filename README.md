# Patient Task Tracker

A simple C# console application to manage patients and their associated tasks. This project demonstrates clean object-oriented design, encapsulation, and separation of concerns using manager classes. It is intended as a learning project and a foundation for future enhancements (such as file/database storage or a web interface).

---

## Features

- **Add, list, edit, and remove patients**
- **Add, list, edit, and remove tasks**
- **Assign tasks to patients**
- **Mark tasks as complete**
- **Input validation for IDs and dates**
- **Auto-incrementing unique IDs for patients and tasks**

---

## How It Works

- Patients and tasks are managed in memory using manager classes (`PatientManager`, `TaskManager`).
- Each patient and task has a unique, auto-incremented ID.
- The app runs in a menu-driven loop in the console, prompting the user for actions and input.

---

## Usage

### **Requirements**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- Windows (tested), but should work on any platform supporting .NET 8.0+

### **Running the App**
1. Clone the repository:

2. Build and run:

---

## Future Improvements

- Add data persistence (save/load to file or database)
- Implement a web or GUI interface
- Add user authentication and permissions
- Unit tests for manager classes
- CI/CD integration with GitHub Actions

---

## Author

Honore Solomon  
[LinkedIn](https://www.linkedin.com/in/honore-solomon/)  
(Bachelor of Science in Computer Science, Western Governors University, April 2025)

---

## License

This project is open source and available under the [MIT License](LICENSE).
