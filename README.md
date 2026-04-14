# PersonRegister

PersonRegister is a Windows Presentation Foundation (WPF) desktop application built with C# and .NET. It allows you to efficiently manage personal contact information, supporting multiple addresses, phone numbers, and customizable data fields.

## Features

- **Person Management**: Add, edit, and keep track of people's basic information such as First Name, Last Name, and Date of Birth.
- **Multiple Addresses & Phones**: Store multiple addresses (Home, Office, etc.) and phone numbers for each person.
- **Custom Fields**: Add custom metadata or specific unique fields to individual records.
- **CSV Data Import/Export**: Easily load data from or export data to CSV files. Includes validation to ensure no duplicate IDs are loaded.
- **Unique ID Generation**: Automatically generates and enforces unique IDs for every person to guarantee data integrity.

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (Compatible with the project's target framework)
- Visual Studio 2022 (Recommended) or another compatible C# IDE.

### Installation & Execution

1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/jpr-otter/person-register
   ```
2. Navigate to the project directory:
   ```bash
   cd /PersonRegister
   ```
3. Open the `PersonRegister.sln` solution file in Visual Studio.
4. Build the solution (`Ctrl + Shift + B`).
5. Run the application (`F5` or `Ctrl + F5`).

## Usage

- **Adding a Person**: Click the Add button (or equivalent in the UI) to create a new person entry. A unique ID will be automatically generated.
- **Importing Data**: Use the provided demo files (`demoDataForPersonRegister.txt`) to explore the CSV import functionality.



