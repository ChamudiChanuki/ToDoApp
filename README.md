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

This project includes unit, integration, and controller tests for the Todo API using xUnit, FluentAssertions, and Moq.

🔧 Test Types

Unit Tests — verify service-layer logic (TaskServiceTests)

Integration Tests — verify full API behavior using in-memory SQLite (TasksControllerTests under Integration/)

▶️ Running Tests with Code Coverage

Run all tests and collect coverage

dotnet test tests/Todo.Api.Tests/Todo.Api.Tests.csproj --configuration Release --collect:"XPlat Code Coverage"


Builds the test project in Release mode

Executes all xUnit tests (unit, integration, controller)

Collects coverage data in Cobertura XML format inside TestResults/

Install the ReportGenerator tool

dotnet tool update -g dotnet-reportgenerator-globaltool


Installs (or updates) the global report generator

Converts raw coverage results into human-readable HTML reports

Generate the HTML coverage report

reportgenerator -reports:"tests/**/TestResults/**/coverage.cobertura.xml" -targetdir:"coverage" -reporttypes:Html;HtmlSummary


Reads coverage results from all test projects under tests/

Outputs reports to a folder named coverage/

Generates both detailed (Html) and summary (HtmlSummary) views

Open the coverage report

.\coverage\index.html


Opens a visual coverage summary in your browser

📊 Notes

Line Coverage → percentage of lines executed at least once

Branch Coverage → percentage of conditional branches (if/else) executed

<img width="1337" height="678" alt="image" src="https://github.com/user-attachments/assets/35bb53fc-5c34-42fa-ad75-175177f661bd" />


