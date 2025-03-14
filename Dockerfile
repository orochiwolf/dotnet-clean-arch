# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build the project
RUN dotnet build -c Release --no-restore

# Publish the app
RUN dotnet publish -c Release -o out --no-restore

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/out .

# Expose port
EXPOSE 5000

# Entry point
ENTRYPOINT ["dotnet", "DotnetCleanAPI.dll"]