# Use the official .NET SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy everything and restore
COPY . . 

# Restore NuGet packages
RUN dotnet restore

# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development


# Build and publish
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out .

# Expose port
EXPOSE 8080

# Run the API
ENTRYPOINT ["dotnet", "DotnetCleanAPI.dll"]
