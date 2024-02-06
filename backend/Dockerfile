# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . .

RUN dotnet restore

WORKDIR /app/ExpenseTrackerApi
RUN dotnet tool install --global dotnet-ef --version 8.0.1
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef database update --context DatabaseContext

WORKDIR /app

RUN dotnet publish -c Release -o out

# Migration stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migration
WORKDIR /app

COPY --from=build-env /app/out .

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "ExpenseTrackerApi.dll"]
