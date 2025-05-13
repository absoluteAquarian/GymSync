# Use the official .NET 9 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Render expects apps to listen on port 10000
EXPOSE 7172
ENV ASPNETCORE_URLS=http://+:7172
ENV DOTNET_URLS=http://+:7172

# Copy published output from GitHub Actions
COPY publish/ ./

# Start the application
ENTRYPOINT ["dotnet", "GymSync.dll"]
