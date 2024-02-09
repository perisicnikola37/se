# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . .

RUN dotnet restore

WORKDIR /app

RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "ExpenseTrackerApi.dll"]
