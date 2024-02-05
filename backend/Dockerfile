FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /backend

COPY *.csproj ./

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release -o out

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        wkhtmltopdf \
        wkhtmltoimage \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /backend
COPY --from=build-env /backend/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "ExpenseTrackerApi.dll"]
