# dotnet-clean-arch
🚀 Advanced .NET Clean Architecture API

This repository contains a .NET API built with Clean Architecture, Entity Framework Core, JWT Authentication, and Docker support.

🏗️ Project Structure

.
├── Dockerfile

├── Program.cs

├── AppDbContext.cs

├── Controllers/

├── Services/

└── Models/

🎯 Features

✅ Clean Architecture
✅ Entity Framework Core (In-Memory Database)
✅ JWT Authentication
✅ Dependency Injection
✅ Docker Support
✅ Swagger UI

🛠️ Getting Started

1️⃣ Clone the repository

git clone https://github.com/username/dotnet-clean-api.git
cd dotnet-clean-api

2️⃣ Build Docker Image

docker build -t dotnet-api .

3️⃣ Run the container

docker run -p 5000:5000 dotnet-api

4️⃣ Access the API

Swagger UI: http://localhost:5000/swagger

API Endpoints: http://localhost:5000/api/

📦 Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000
ENTRYPOINT ["dotnet", "DotnetCleanAPI.dll"]

📚 Tech Stack

.NET 8.0

Entity Framework Core

JWT Authentication

Docker

📜 License

Apache License 2.0

