# ToDo API

📖 **Overview**  
This project is a robust **ToDo API** developed with **ASP.NET Core Web API** and **.NET 8**, designed to efficiently manage tasks in a user-centric to-do list application. The API supports features like creating, reading, updating, and deleting tasks, along with batch updates for task statuses. The backend database is powered by **MSSQL**, ensuring reliable data storage and retrieval. Additionally, the application is **Dockerized**, allowing for simple setup, deployment, and scalability across various environments.

✨ **Features**

### Endpoints:

- **POST /tasks**  
  Create a new to-do item for a user.
- **GET /tasks/{id}**  
  Retrieve a specific to-do item by its ID.
- **GET /tasks**  
  Retrieve all to-do items associated with a specific user.
- **PUT /tasks**  
  Update an existing to-do item with new information.
- **PUT /tasks/status**  
  Update the status of multiple to-do items at once (e.g., mark tasks as complete/incomplete).
- **DELETE /tasks/{id}**  
  Delete a specific to-do item by its ID.
- **DELETE /tasks**  
  Delete multiple to-do items at once by their IDs.

### Core Functionality:

- **Authentication**:  
  Secure login and token generation using JWT.
- **Authorization**:  
  Role-based access control for restricted endpoints.
- **Task Management**:  
  Efficiently create, update, retrieve, and delete to-do items.
- **Batch Updates**:  
  Update the status of multiple tasks simultaneously.
- **User-Specific Tasks**:  
  All tasks are associated with a specific user, ensuring a personalized task list.
- **Docker Support**:  
  The entire application is fully containerized, allowing for easy deployment and scaling using Docker Compose.

## 🛠️ **Installation and Setup**

### Prerequisites

- **.NET 8 SDK**
- **Docker**
- **Git**

### Clone the Repository

```bash
git clone https://github.com/shielamae02/ToDo-API.git
cd ToDo-API
```

### Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=your-database;User Id=your-username;Password=your-password;"
  }
}
```

Review the `docker-compose.yml` files to ensure they match your requirements.

### Running the Application Locally

⚙️ Without Docker

1. Build and run the program
   ```bash
   dotnet build
   dotnet run
   ```

🐳 With Docker

1. Create a .env file in the root of your project and populate it with the following values, as shown in the env.sample file:

   ```
   # Database connection string.
   DEFAULT_CONNECTION=""

   # JWT Configuration
   JWT_SECRET=""                         # Secret key for signing JWTs.
   JWT_ISSUER=""                         # Issuer claim for JWTs.
   JWT_AUDIENCE=""                       # Audience claim for JWTs.
   JWT_ACCESSEXPIRY=3                    # Access token expiry (hours).
   JWT_REFRESHEXPIRY=30                  # Refresh token expiry (days).
   ```

   Update the placeholders with your environment-specific values.

1. Build and run the Docker containers:

   ```bash
   docker-compose up --build
   ```

---

## 📩 Contact

For any questions or feedback, feel free to reach out:

- Email: shiela.mlepon@gmail.com
- GitHub: shielamae02
