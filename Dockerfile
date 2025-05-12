# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR .

# Copy csproj and restore
COPY ["Gymsync.csproj", "./"]
RUN dotnet restore "Gymsync.csproj"

# Copy everything and publish
COPY . .
RUN dotnet publish "Gymsync.csproj" -c Release -o /publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Gymsync.dll"]