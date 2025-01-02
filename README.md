# MessagingApp

MessagingApp is a proof-of-concept messaging application designed to demonstrate key concepts of distributed systems, including messaging and scalability. The backend is built with ASP.NET Core and uses PostgreSQL as the database.

---

## Features

- Publish and consume messages using a producer-consumer pattern.
- REST API for message creation and retrieval.
- Database migrations handled via DbUp.
- Designed to be extensible for distributed systems concepts.

---

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- Docker Desktop [(terminal instructions)](https://docs.docker.com/engine/install/ubuntu/#install-using-the-repository)
- PostgreSQL [(terminal instructions)](https://www.postgresql.org/download/linux/ubuntu/)
- pgAdmin (terminal instructions below, or [download here](https://www.pgadmin.org/download/))
  - `docker pull dpage/pgadmin4`
  - `docker run --name pgadmin-container -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=user@domain.com -e PGADMIN_DEFAULT_PASSWORD=catsarecool -d dpage/pgadmin4`
  - Save the host name/address from this command `docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' pgadmin-container`
  - Open pgAdmin from docker ([should be this address](http://localhost:5050)), sign in (with credentials from string above), and create a local DB server using the host name/address from previous step
- RabbitMQ [(terminal instructions)](https://www.rabbitmq.com/docs/download)
- .NET SDK [(terminal instructions)](https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install)
  - Check that it was installed properly with `dotnet --version`
  - If it installed successfully but the `dotnet` command isn't recognized, then add the path to dotnet in your environment variables by adding `export PATH="$PATH:$HOME/.dotnet"` to your .bashrc file
- A code editor, such as [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio Code](https://code.visualstudio.com/).

---

## Installation 

### Clone the Repository

```bash
git clone https://github.com/dakota-bayer/MessagingAppBackend.git
cd messaging-app
```

### Install Backend Dependencies

1. Navigate to the backend folder:

   ```bash
   cd MessagingAppBackend
   ```

2. Restore NuGet packages:

   ```bash
   dotnet restore
   ```

### Configure PostgreSQL

1. Create a new PostgreSQL database called `MessagingDB`:

   ```sql
   CREATE DATABASE MessagingDB;
   ```

2. Update the connection string in the `appsettings.json` file:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=MessagingDB;Username=postgres;Password=yourpassword"
   }
   ```

### Run Database Migrations

1. Navigate to the `MigrationRunner` project:

   ```bash
   cd MigrationRunner
   ```

2. Run the migrations:

   ```bash
   dotnet run
   ```

---

## Running the Application

1. Start the backend API:

   ```bash
   cd MessagingAppBackend
   dotnet run
   ```

2. Access the API endpoints:
    - Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
    - Example endpoints:
        - GET `/api/messages`
        - POST `/api/messages`

---

## Front-End Setup (Optional)

If you’re developing a front-end for this app using Angular:

1. Navigate to the front-end folder:

   ```bash
   cd MessagingAppFrontend
   ```

2. Install npm dependencies:

   ```bash
   npm install
   ```

3. Start the Angular development server:

   ```bash
   ng serve
   ```

4. Access the front-end at [http://localhost:4200](http://localhost:4200).

---

## Technologies Used

- **Backend**: ASP.NET Core, C#
- **Database**: PostgreSQL
- **Migrations**: DbUp
- **Front-End**: Angular (optional)
- **Documentation**: Swagger

---

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

