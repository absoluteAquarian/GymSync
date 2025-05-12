# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR ./

# Copy csproj and restore
COPY ["GymSync.csproj", "./"]
RUN dotnet restore "GymSync.csproj"

# Copy everything and publish
COPY . .
RUN dotnet publish "GymSync.csproj" -c Release -o /app/publish --verbosity diagnostic

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GymSync.dll"]