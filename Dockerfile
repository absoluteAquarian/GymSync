# Use the pre-built image from Docker Hub (existing image)
FROM ${{ secrets.DOCKERHUB_USERNAME }}/gymsync:latest

# Set the working directory
WORKDIR /app

# Expose port 80 for the app to be accessible
EXPOSE 80

# Set the entry point for running the app
ENTRYPOINT ["dotnet", "GymSync.dll"]