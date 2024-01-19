FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /backend

COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /backend
COPY --from=build-env /backend/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "backend.dll"]