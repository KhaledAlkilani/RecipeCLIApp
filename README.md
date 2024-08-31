This is a C# CLI application for managing recipes. The application interacts with a PostgreSQL database, and both the app and the database can be run using Docker containers.

Prerequisites:

Before getting started, ensure you have the following installed:

- .NET SDK 8.0 or later
- Visual Studio 2022: to open, clean, and build the solution.
- Docker Desktop: for building and running Docker containers.

Getting Started
1. Clone the Repository
First, clone the repository to your local machine:

git clone https://github.com/KhaledAlkilani/RecipeCLIApp.git
cd RecipeCLIApp

2. Clean and Build the Solution
Open the cloned project in Visual Studio.

Clean the Solution: Go to Build > Clean Solution.
Build the Solution: Go to Build > Build Solution.
This will ensure all dependencies are restored and the project is set up correctly.

3. Set Environment Variables in Visual Studio
To run the application locally in Visual Studio, you need to add the environment variables in the Debug tab:

Right-click on the project in Solution Explorer and select "Properties."

Navigate to the "Debug" tab.

In the "Environment Variables" section, add the following:

Name: POSTGRES_USER, Value: your_postgres_user
Name: POSTGRES_PASSWORD, Value: your_postgres_password
Name: POSTGRES_DB, Value: your_postgres_db
Name: CONNECTION_STRING_LOCAL, Value: Host=localhost;Port=5432;Database=your_postgres_db;Username=your_postgres_user;Password=your_postgres_password;Timeout=10;SslMode=Prefer;
Name: ASPNETCORE_ENVIRONMENT, Value: Development

4. Create and Configure the .env File
In order to configure the database connection for Docker containers, create a .env file in the root of the project directory. This file should contain the following environment variables, customized with your own PostgreSQL credentials:

POSTGRES_USER=your_postgres_user
POSTGRES_PASSWORD=your_postgres_password
POSTGRES_DB=your_postgres_db
CONNECTION_STRING_LOCAL=Host=localhost;Port=5432;Database=your_postgres_db;Username=your_postgres_user;Password=your_postgres_password;Timeout=10;SslMode=Prefer;
CONNECTION_STRING_DOCKER=Host=db;Port=5432;Database=your_postgres_db;Username=your_postgres_user;Password=your_postgres_password;Timeout=10;SslMode=Prefer;

5. Pull Docker App Image from Docker Hub.
Ensure docker desktop application is open.
To pull the pre-built Docker image of the app from Docker Hub:

docker push khaled963/recipecliapp:latest

6. Build and Run Docker Containers
Open a command line in the project directory and use the following commands to build and run the Docker containers:

docker-compose up -d --build

This command will:

Build the Docker image for the application.
Start up the PostgreSQL database container.
Start up the application container.

7. Verify Containers Are Running
To verify that the containers are up and running, execute:

docker ps

Ensure that both the recipecliapp-app and recipecliapp-db containers are running.

8. Run the Application
After ensuring the containers are running, you can run the application using the following command:

dotnet run

You should now be able to interact with the application through the command line.
