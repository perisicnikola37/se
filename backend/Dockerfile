FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /backend

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /backend
COPY --from=build-env /backend/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "backend.dll"]it
