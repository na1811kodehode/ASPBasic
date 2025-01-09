# Use the official .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["tutorial2.csproj", "./"]
RUN dotnet restore

# Copy the entire application and publish it
COPY . .
RUN dotnet publish -c Release -o /app

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Start the application
ENTRYPOINT ["dotnet", "tutorial2.dll"]