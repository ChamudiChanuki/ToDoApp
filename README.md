🧩 To-Do Task Web Application

A small full-stack app that lets you add tasks, list the 5 most recent active tasks, and mark them Done.
Everything runs via Docker Compose (DB, API, UI). Includes backend (xUnit) and frontend (Jest/RTL) tests.


🧱 Architecture
| Layer                | Technology                       | Description                               |
| -------------------- | -------------------------------- | ----------------------------------------- |
| Frontend             | React  (Vite)                    | SPA for user interactions                 |
| Backend              | .NET 8 Web API                   | REST API managing task operations         |
| Database             | MySQL 8                          | Stores task data                          |
| Tests                | Jest (frontend), xUnit (backend) | Unit and integration coverage             |
| Containerization     | Docker & Docker Compose          | Single-command startup for all components |


🗂 Project Structure

<img width="696" height="614" alt="image" src="https://github.com/user-attachments/assets/fd4ea927-8401-4c3c-88a5-d3e54e92e3ae" />




🗃️ Database Schema

Table: Task
| Column        | Type               | Description             |
| ------------- | ------------------ | ----------------------- |
| `Id`          | INT (PK, Identity) | Unique identifier       |
| `Title`       | NVARCHAR(100)      | Task title              |
| `Description` | NVARCHAR(500)      | Task description        |
| `IsCompleted` | BIT                | Task completion status  |
| `CreatedAt`   | DATETIME           | Task creation timestamp |


⚙️ Setup and Run Instructions
Prereqs: Docker Desktop + Docker Compose.

From the repository root:

build + start all services (db, api, frontend):
docker compose up --build

URLs:

Frontend: [http://localhost:8080](http://localhost:3000/)

Backend (Swagger): [http://localhost:5000/swagger](http://localhost:5000/swagger/index.html)

Stop & clean up/:

docker compose down

🧪 Testing

API Testing & Code Coverage

This project uses xUnit for testing, FluentAssertions for readable assertions, and ReportGenerator for code-coverage reports.

▶️ Running Tests with Code Coverage

1️⃣ Run all tests and collect coverage
dotnet test tests/Todo.Api.Tests/Todo.Api.Tests.csproj --configuration Release --collect:"XPlat Code Coverage"

This command:

Builds the test project in Release mode

Runs all xUnit tests

Collects coverage data in Cobertura XML format (saved under TestResults)

2️⃣ Install the report generator tool
dotnet tool update -g dotnet-reportgenerator-globaltool


This installs (or updates) the global ReportGenerator tool that converts the raw coverage data into human-readable HTML reports.

3️⃣ Generate the HTML report
reportgenerator -reports:"tests/*/TestResults/*/coverage.cobertura.xml" -targetdir:"coverage" -reporttypes:Html;HtmlSummary

This command:

Reads coverage results from any test projects under tests/

Outputs the report to a folder called coverage/

Generates both detailed (Html) and summary (HtmlSummary) reports

4️⃣ View the coverage report

After generation, open the report in your browser:

.\coverage\index.html

You’ll see line-coverage and branch-coverage percentages for each file, class, and method.


🧩 Notes

Line Coverage = lines of code executed at least once

Branch Coverage = both sides of if/else conditions executed

Coverage data is stored automatically under tests/Todo.Api.Tests/TestResults/

To increase branch coverage, add test cases for all alternate paths (e.g., invalid input, not-found scenarios).

<img width="1337" height="678" alt="image" src="https://github.com/user-attachments/assets/35bb53fc-5c34-42fa-ad75-175177f661bd" />


