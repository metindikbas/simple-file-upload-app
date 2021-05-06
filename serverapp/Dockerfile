FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

COPY ./src/ ./
RUN dotnet restore Api/Api.csproj

RUN dotnet publish -c Release -o out Api/Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SimpleFileUpload.Api.dll"]