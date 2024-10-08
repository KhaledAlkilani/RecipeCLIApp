﻿# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /app
COPY --from=build-env /app/out .

# Set environment variables for database connection
ARG CONNECTION_STRING_DOCKER
ENV ConnectionStrings__DefaultConnection=$CONNECTION_STRING_DOCKER

ENTRYPOINT ["dotnet", "RecipeCLIApp.dll"]
