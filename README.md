# CareerCompassWebApi

CareerCompassWebApi is a .NET 9 Web API for user authentication, registration, and profile management. It uses Entity Framework Core, JWT authentication, and follows clean architecture principles.

## Features
- User registration and login with JWT authentication
- Secure password hashing
- User profile management (view/update)
- RESTful endpoints
- Entity Framework Core with SQL Server
- DTOs for request/response models

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server
- Git

### Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/devzwide/CareerCompassWebApi.git
   cd CareerCompassWebApi
   ```
2. Configure your connection string and JWT secrets in `appsettings.Development.json` or use User Secrets for sensitive data.
3. Run database migrations:
   ```bash
   dotnet ef database update
   ```
4. Build and run the API:
   ```bash
   dotnet build
   dotnet run
   ```

### API Endpoints
- `POST /api/Auth/register` — Register a new user
- `POST /api/Auth/login` — Login and receive JWT token
- `GET /api/Users/me` — Get current user's profile (JWT required)
- `PUT /api/Users/me` — Update current user's profile (JWT required)

See `WebAPI.http` for example requests using VS Code REST Client.

## Project Structure
- `Application/` — DTOs, interfaces, and services
- `Core/` — Entity models and repository interfaces
- `Infrastructure/` — Data context and repository implementations
- `Presentation/` — API controllers
- `Migrations/` — Entity Framework migrations

## Contributing
Pull requests are welcome. For major changes, open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License.
