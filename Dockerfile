FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY TestWebApiClient.csproj TestWebApiClient.csproj
RUN dotnet restore TestWebApiClient.csproj
COPY . .
WORKDIR /src
RUN dotnet build TestWebApiClient.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TestWebApiClient.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TestWebApiClient.dll"]