# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY CargoPay.sln ./
COPY CargoPay.Application/*.csproj CargoPay.Application/
COPY CargoPay.Domain/*.csproj CargoPay.Domain/
COPY CargoPay.Infrastructure/*.csproj CargoPay.Infrastructure/
COPY CargoPay.Presentation/*.csproj CargoPay.Presentation/
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./
WORKDIR /app/CargoPay.Presentation
RUN dotnet publish -c Release -o out

# Use a runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/CargoPay.Presentation/out ./
EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "CargoPay.Presentation.dll"]
