# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj files and restore dependencies
COPY ParkManager.API/ParkManager.API.csproj ParkManager.API/
COPY ParkManager.Domain/ParkManager.Domain.csproj ParkManager.Domain/
COPY ParkManager.Infrastructure/ParkManager.Infrastructure.csproj ParkManager.Infrastructure/
COPY ParkManager.Application/ParkManager.Application.csproj ParkManager.Application/

RUN dotnet restore ParkManager.API/ParkManager.API.csproj

# Copy the rest of the application source code
COPY . .

# Build the application
RUN dotnet publish ParkManager.API/ParkManager.API.csproj -c Release -o out

# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published application from the build environment
COPY --from=build-env /app/out .

# Expose the port the app runs on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ParkManager.API.dll"]
