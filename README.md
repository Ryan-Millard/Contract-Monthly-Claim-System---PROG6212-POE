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

## File Structure (Click to expand/collapse)
<section>
  <details>
    <summary>ContractMonthlyClaimSystem</summary>
    <ul>
      <li>bin
        <ul>
          <li>Debug
            <ul>
              <li>net8.0
                <ul>
                  <li>cs</li>
                  <li>de</li>
                  <li>es</li>
                  <li>fr</li>
                  <li>it</li>
                  <li>ja</li>
                  <li>ko</li>
                  <li>pl</li>
                  <li>pt-BR</li>
                  <li>ru</li>
                  <li>runtimes
                    <ul>
                      <li>win
                        <ul>
                          <li>lib
                            <ul>
                              <li>net8.0</li>
                            </ul>
                          </li>
                        </ul>
                      </li>
                      <li>win-x64
                        <ul>
                          <li>native</li>
                        </ul>
                      </li>
                    </ul>
                  </li>
                  <li>tr</li>
                  <li>zh-Hans</li>
                  <li>zh-Hant</li>
                </ul>
              </li>
            </ul>
          </li>
        </ul>
      </li>
      <li>Data</li>
      <li>Middleware</li>
      <li>Migrations</li>
      <li>Models
        <ul>
          <li>Enums</li>
        </ul>
      </li>
      <li>obj
        <ul>
          <li>Debug
            <ul>
              <li>net8.0
                <ul>
                  <li>ref</li>
                  <li>refint</li>
                  <li>scopedcss
                    <ul>
                      <li>bundle</li>
                      <li>Pages
                        <ul>
                          <li>Shared</li>
                        </ul>
                      </li>
                      <li>projectbundle</li>
                    </ul>
                  </li>
                  <li>staticwebassets</li>
                </ul>
              </li>
            </ul>
          </li>
        </ul>
      </li>
      <li>Pages
        <ul>
          <li>Dashboard
            <ul>
              <li>Lecturer</li>
            </ul>
          </li>
          <li>Shared</li>
          <li>Users</li>
        </ul>
      </li>
      <li>Properties</li>
      <li>Services</li>
      <li>uploads
        <ul>
          <li>1</li>
          <li>2</li>
          <li>3</li>
          <li>4</li>
          <li>5</li>
          <li>6</li>
          <li>7</li>
          <li>8</li>
          <li>9</li>
        </ul>
      </li>
      <li>Validation</li>
      <li>wwwroot
        <ul>
          <li>css</li>
          <li>images</li>
          <li>js</li>
          <li>lib
            <ul>
              <li>bootstrap
                <ul>
                  <li>dist
                    <ul>
                      <li>css</li>
                      <li>js</li>
                    </ul>
                  </li>
                </ul>
              </li>
              <li>jquery
                <ul>
                  <li>dist</li>
                </ul>
              </li>
              <li>jquery-validation
                <ul>
                  <li>dist</li>
                </ul>
              </li>
              <li>jquery-validation-unobtrusive</li>
            </ul>
          </li>
          <li>uploads
            <ul>
              <li>1</li>
              <li>2</li>
              <li>3</li>
              <li>4</li>
              <li>5</li>
              <li>6</li>
              <li>7</li>
              <li>8</li>
              <li>9</li>
            </ul>
          </li>
        </ul>
      </li>
    </ul>
  </details>

  <details>
    <summary>ContractMonthlyClaimSystem.Tests</summary>
    <ul>
      <li>bin
        <ul>
          <li>Debug
            <ul>
              <li>net8.0
                <ul>
                  <li>cs</li>
                  <li>de</li>
                  <li>es</li>
                  <li>fr</li>
                  <li>it</li>
                  <li>ja</li>
                  <li>ko</li>
                  <li>pl</li>
                  <li>pt-BR</li>
                  <li>ru</li>
                  <li>runtimes
                    <ul>
                      <li>win
                        <ul>
                          <li>lib
                            <ul>
                              <li>netstandard2.0</li>
                            </ul>
                          </li>
                        </ul>
                      </li>
                    </ul>
                  </li>
                  <li>tr</li>
                  <li>zh-Hans</li>
                  <li>zh-Hant</li>
                </ul>
              </li>
            </ul>
          </li>
        </ul>
      </li>
      <li>Data</li>
      <li>Middleware</li>
      <li>Models
        <ul>
          <li>Enums</li>
        </ul>
      </li>
      <li>obj
        <ul>
          <li>Debug
            <ul>
              <li>net8.0
                <ul>
                  <li>ref</li>
                  <li>refint</li>
                </ul>
              </li>
            </ul>
          </li>
        </ul>
      </li>
      <li>Pages</li>
      <li>Validation</li>
    </ul>
  </details>
</section>


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

## Contributing

No contributions may be made by other as this is a solo assessment.

## License

This project is licensed under the [GNU General Public License v3.0](LICENSE).

## Contact

For any questions or issues, please contact Ryan Millard at [millardyryandevon@gmail.com](mailto:millardyryandevon@gmail.com).
