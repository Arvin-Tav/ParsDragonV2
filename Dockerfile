# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file
COPY *.sln ./

# Copy all the project files
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
COPY ["Learning.Application/Learning.Application.csproj", "Learning.Application/"]
COPY ["Learning.Domain/Learning.Domain.csproj", "Learning.Domain/"]
COPY ["Learning.Infra.Data/Learning.Infra.Data.csproj", "Learning.Infra.Data/"]
COPY ["Learning.Infra.Ioc/Learning.Infra.Ioc.csproj", "Learning.Infra.Ioc/"]
COPY ["Learning.Mvc/Learning.Mvc.csproj", "Learning.Mvc/"]
# Add additional projects as needed

# Restore dependencies for the solution
RUN dotnet restore

# Copy the rest of the solution files
COPY . ./

ENV ASPNETCORE_ENVIRONMENT=Production

# Build and publish only Learning.Mvc
WORKDIR /app/Learning.Mvc
RUN dotnet publish -c Release -o /out

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /out .

# Expose the port the application runs on
EXPOSE 5000

# Set the entrypoint to run the application with --urls option
ENTRYPOINT ["dotnet", "Learning.Mvc.dll", "--urls", "http://0.0.0.0:5000"]
