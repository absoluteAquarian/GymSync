# Define build argument for image name
ARG IMAGE_TAG

# Use the pre-built image from Docker Hub (existing image)
FROM absoluteaquarian/gymsync:${IMAGE_TAG}

# Set the working directory
WORKDIR /app

# Expose port 80 for the app to be accessible
EXPOSE 80

# Set the entry point for running the app
ENTRYPOINT ["dotnet", "GymSync.dll"]