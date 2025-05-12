# Base image for ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published output from GitHub Actions
COPY ./publish/ .

# Set the startup command
ENTRYPOINT ["dotnet", "GymSync.dll"]