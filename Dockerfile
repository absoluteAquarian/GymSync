ARG CERT_PASSWORD

# Use the official .NET 9 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Render expects apps to listen on port 10000
EXPOSE 7172
ENV ASPNETCORE_URLS=http://+:7172
ENV DOTNET_URLS=http://+:7172

# Copy published output from GitHub Actions
COPY publish/ ./

# Copy the certificate into the image
COPY certs/aspnetapp.pfx /https/aspnetapp.pfx

# Set environment variables for Kestrel to use the certificate
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=${CERT_PASSWORD}

# Start the application
ENTRYPOINT ["dotnet", "GymSync.dll"]
