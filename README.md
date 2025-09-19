
# 🚗 ReCapProject
**ReCapProject** is a sample application developed in C# that adheres to layered architecture principles and follows the SOLID principles. The project utilizes Entity Framework for data access, implements FluentValidation for model validation, and employs JWT (JSON Web Token) for secure authentication. The application is designed to operate through both console and Web API interfaces

![GitHub stars](https://img.shields.io/github/stars/kdrturan/ReCapProject?style=social)
![GitHub forks](https://img.shields.io/github/forks/kdrturan/ReCapProject?style=social)

---

## 🧱 Project Structure
The project is organized into the following layer:

- **Business** Contains business logic and service implementation.
- **DataAccess** Handles data access using Entity Framewor.
- **Entities** Holds entity classes representing database table.
- **Core** Includes common infrastructure code such as helper classes and base service.
- **ConsoleUI** Provides a console interface for the applicatio.
- **WebAPI** Exposes RESTful API service.

---

## 🛠️ Technologies Use

| Technology            | Description                                         |
|-----------------------|-----------------------------------------------------|
| C# (.NET)             | Programming language and platform                   |
| Entity Framework      | Object-Relational Mapping (ORM) tool                |
| FluentValidation      | Library for model validation                        |
| JWT (JSON Web Token)  | Secure authentication method                        |
| Layered Architecture  | Design principle for organizing application layers  |
| SOLID Principles      | Five fundamental principles of object-oriented design |
| RESTful API           | Architectural style for web services               |

---

## ⚙️ Installation and Setup

. Clone the repositoy:

   ```bash
   git clone https://github.com/kdrturan/ReCapProject.git
   ```

. Open `ReCapProject.sln` in Visual Studo.

. Restore the required NuGet packags.

. Configure the database connection in the `appsettings.json` fie.

. Apply migrations to create the databae:

   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```

. Run the applicatin.

---

## 🔐 Security and Validation

- **FluentValidation*: Used for validating models, ensuring that input data meets defined rules. For example, checking required fields during user loin.

- **JWT (JSON Web Token)*: Employed for user authentication and authorization. It provides a token-based authentication system to secure accss.

---


