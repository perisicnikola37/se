FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /backend

COPY *.csproj ./
COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

RUN apt-get update \
    && apt-get upgrade -y \
    && apt-get install -y wget \
    && wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6.1-2/wkhtmltox_0.12.6.1-2.jammy_amd64.deb \
    && apt install -y -f ./wkhtmltox_0.12.6.1-2.jammy_amd64.deb \
    && rm -rf wkhtmltox_0.12.6.1-2.jammy_amd64.deb \
    && rm -rf /var/lib/apt/lists/*


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /backend
COPY --from=build-env /backend/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "ExpenseTrackerApi.dll"]
