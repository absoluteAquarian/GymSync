# Use the official .NET 9 runtime image as the base for the final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy the pre-built files from the host system (published output)
COPY ./publish/ .

# Check the contents of the /app directory to ensure GymSync.dll exists
RUN ls -al /app

# Set the entry point to start the application
ENTRYPOINT ["dotnet", "GymSync.dll"]