# Contract Monthly Claim System (CMCS)

## Overview

The Contract Monthly Claim System (CMCS) is a .NET web-based application designed to streamline the process of submitting and approving monthly claims for Independent Contractor lecturers. The system is built using ASP.NET Core with Razor Pages for the user interface, and it follows a layered architecture to ensure scalability, performance, and compatibility. The application is currently in the prototype stage and is designed with up to 1000 active users in mind.

## Features

- **Role-Based Access**: Different templates and views are rendered based on user roles, including lecturers, and managers.
- **Layered Architecture**: Utilizes a clear separation between the Presentation Layer (Razor Pages), Business Logic Layer (Services), and Data Access Layer (Entity Framework Core).
- **Form Validation**: Includes robust form validation for claim submission.
- **Security**: Employs ASP.NET Core Identity for user authentication and authorization.
- **Styling**: Styled using Bootstrap for a responsive and modern user experience.
- **Database**: SQL Server is used for managing data, with entities such as Users, Claims, ClaimStatus, Documents, and Courses.

## File Structure
```
├───ContractMonthlyClaimSystem
│   ├───bin
│   │   └───Debug
│   │       └───net8.0
│   │           ├───cs
│   │           ├───de
│   │           ├───es
│   │           ├───fr
│   │           ├───it
│   │           ├───ja
│   │           ├───ko
│   │           ├───pl
│   │           ├───pt-BR
│   │           ├───ru
│   │           ├───runtimes
│   │           │   ├───win
│   │           │   │   └───lib
│   │           │   │       └───net8.0
│   │           │   └───win-x64
│   │           │       └───native
│   │           ├───tr
│   │           ├───zh-Hans
│   │           └───zh-Hant
│   ├───Data
│   ├───Middleware
│   ├───Migrations
│   ├───Models
│   │   └───Enums
│   ├───obj
│   │   └───Debug
│   │       └───net8.0
│   │           ├───ref
│   │           ├───refint
│   │           ├───scopedcss
│   │           │   ├───bundle
│   │           │   ├───Pages
│   │           │   │   └───Shared
│   │           │   └───projectbundle
│   │           └───staticwebassets
│   ├───Pages
│   │   ├───Dashboard
│   │   │   └───Lecturer
│   │   ├───Shared
│   │   └───Users
│   ├───Properties
│   ├───Services
│   ├───uploads
│   ├───Validation
│   └───wwwroot
│       ├───css
│       ├───images
│       ├───js
│       ├───lib
│       │   ├───bootstrap
│       │   │   └───dist
│       │   │       ├───css
│       │   │       └───js
│       │   ├───jquery
│       │   │   └───dist
│       │   ├───jquery-validation
│       │   │   └───dist
│       │   └───jquery-validation-unobtrusive
│       └───uploads
│           ├───1
│           ├───2
│           ├───3
│           ├───4
│           ├───5
│           ├───6
│           ├───7
│           ├───8
│           └───9
└───ContractMonthlyClaimSystem.Tests
    ├───bin
    │   └───Debug
    │       └───net8.0
    │           ├───cs
    │           ├───de
    │           ├───es
    │           ├───fr
    │           ├───it
    │           ├───ja
    │           ├───ko
    │           ├───pl
    │           ├───pt-BR
    │           ├───ru
    │           ├───runtimes
    │           │   └───win
    │           │       └───lib
    │           │           └───netstandard2.0
    │           ├───tr
    │           ├───zh-Hans
    │           └───zh-Hant
    ├───Data
    ├───Middleware
    ├───Models
    │   └───Enums
    ├───obj
    │   └───Debug
    │       └───net8.0
    │           ├───ref
    │           └───refint
    ├───Pages
    └───Validation
```

## Dependencies
### `ContractMonthlyClaimSystem` Project
| Top-level Package                                    | Requested | Resolved |
|------------------------------------------------------|-----------|----------|
| ClosedXML                                            | 0.104.2   | 0.104.2  |
| itext7                                               | 9.0.0     | 9.0.0    |
| itext7.bouncy-castle-adapter                         | 9.0.0     | 9.0.0    |
| Microsoft.AspNetCore.Authentication.Cookies          | 2.2.0     | 2.2.0    |
| Microsoft.AspNetCore.Http                             | 2.2.2     | 2.2.2    |
| Microsoft.EntityFrameworkCore                         | 8.0.8     | 8.0.8    |
| Microsoft.EntityFrameworkCore.InMemory                | 8.0.8     | 8.0.8    |
| Microsoft.EntityFrameworkCore.Proxies                 | 8.0.8     | 8.0.8    |
| Microsoft.EntityFrameworkCore.Tools                   | 8.0.8     | 8.0.8    |
| Pomelo.EntityFrameworkCore.MySql                      | 8.0.2     | 8.0.2    |

### `ContractMonthlyClaimSystem.Tests` Project
| Top-level Package                                | Requested | Resolved |
|--------------------------------------------------|-----------|----------|
| coverlet.collector                               | 6.0.0     | 6.0.0    |
| FluentAssertions                                 | 6.12.1    | 6.12.1  |
| FluentValidation                                 | 11.10.0   | 11.10.0 |
| Microsoft.AspNetCore.Mvc.RazorPages              | 2.2.5     | 2.2.5   |
| Microsoft.EntityFrameworkCore.Relational         | 8.0.8     | 8.0.8   |
| Microsoft.Extensions.Logging.Abstractions        | 8.0.2     | 8.0.2   |
| Microsoft.NET.Test.Sdk                           | 17.6.0    | 17.6.0  |
| Moq                                              | 4.20.72   | 4.20.72 |
| xunit                                            | 2.9.2     | 2.9.2   |
| xunit.runner.visualstudio                        | 2.4.5     | 2.4.5   |


## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) version 6.0 or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or compatible SQL Server instance

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Ryan-Millard/Contract-Monthly-Claim-System---PROG6212-POE.git
   cd src-test
   ```

2. **Install Dependencies**:
   Run the below in the `ContractMonthlyClaimSystem` and `ContractMonthlyClaimSystem.Tests` directories.
   ```bash
   dotnet restore
   ```

4. **Run the Application**:
   Run this code in the `ContractMonthlyClaimSystem` directory.
   ```bash
   dotnet run
   ```

   Navigate to `http://localhost:5252` in your browser to access the application.

## Usage

- **Login**: Users can log in with their credentials. Role-based views will be displayed according to the user's role.
- **Submit Claim**: Lecturers can submit their monthly claims using the submission form in their Dashboard page. Validation ensures all required fields are correctly filled.
- **Approve Claim**: Coordinators and managers can review and approve claims submitted by lecturers in their Dashboard page.

## Project Structure

- `src-tests/` - The root directory of the project.
  - `ContractMonthlyClaimSystem.sln`
  - `ContractMonthlyClaimSystem`
    - `Controllers/` - Contains the controllers for handling requests.
    - `Models/` - Defines the data models used by the application.
    - `Views/` - Contains Razor Pages for the user interface.
    - `wwwroot/` - Static files such as CSS and JavaScript.

## Contributing

No contributions may be made by other as this is a solo assessment.

## License

This project is licensed under the [GNU General Public License v3.0](LICENSE).

## Contact

For any questions or issues, please contact Ryan Millard at [millardyryandevon@gmail.com](mailto:millardyryandevon@gmail.com).
