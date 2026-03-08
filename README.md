# Enrollment Management System (EMS)

A Windows Forms application built with C# for managing student enrollment and user profiles.

## Project Structure

```
EMS/
├── Forms/                      # All Windows Forms and User Controls
│   ├── Dashboard.cs           # Main dashboard interface
│   ├── Login.cs               # User login form
│   ├── RegisterForm.cs        # User registration form
│   ├── Home.cs                # Home user control
│   ├── addstudent.cs          # Add new student form
│   ├── allstudents.cs         # View all students
│   ├── editstudent.cs         # Edit student details
│   ├── allusers.cs            # View all users
│   ├── userprofile.cs         # User profile view
│   ├── edituserprofile.cs     # Edit user profile
│   └── Form1.cs               # Additional form
│
├── Helpers/                    # Utility classes
│   └── Session.cs             # Session management
│
├── Properties/                 # Project properties
│   ├── AssemblyInfo.cs        # Assembly metadata
│   ├── Resources.resx         # Resource files
│   └── Settings.settings      # Application settings
│
├── bin/                        # Build outputs (Debug/Release)
├── obj/                        # Intermediate build files
│
├── Program.cs                  # Application entry point
├── App.config                  # Application configuration
├── packages.config             # NuGet packages
└── EMS.csproj                  # Project file

```

## Technologies Used

- **Framework:** .NET Framework 4.7.2
- **Language:** C#
- **UI:** Windows Forms
- **Database:** MySQL (via MySql.Data 8.3.0)

## Dependencies

- MySql.Data (8.3.0)
- BouncyCastle.Cryptography (2.2.1)
- Google.Protobuf (3.25.1)
- System.Buffers (4.5.1)
- System.Memory (4.5.5)
- Other supporting libraries for async operations and compression

## Getting Started

### Prerequisites

- Visual Studio 2017 or later
- .NET Framework 4.7.2 or higher
- MySQL Server

### Building the Project

1. Open `EMS.csproj` in Visual Studio
2. Restore NuGet packages
3. Build the solution (F6)
4. Run the application (F5)

## Features

- User authentication (Login/Register)
- Student enrollment management
- Add, view, and edit student records
- User profile management
- Dashboard interface
- Session management

## Project Organization

The project has been organized into a clean folder structure:

- **Forms folder** contains all UI-related forms and user controls
- **Helpers folder** contains utility classes like session management
- Configuration and entry point files remain in the root directory

## Last Updated

March 8, 2026
