# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/TaskService.API/TaskService.API.csproj", "TaskService.API/"]
COPY ["src/TaskService.Application/TaskService.Application.csproj", "TaskService.Application/"]
COPY ["src/TaskService.Domain/TaskService.Domain.csproj", "TaskService.Domain/"]
COPY ["src/TaskService.Infrastructure/TaskService.Infrastructure.csproj", "TaskService.Infrastructure/"]

RUN dotnet restore "TaskService.API/TaskService.API.csproj"

# Copy all source files
COPY src/ .

# Build the application
WORKDIR "/src/TaskService.API"
RUN dotnet build "TaskService.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "TaskService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskService.API.dll"]

