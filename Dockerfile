FROM mcr.microsoft.com/dotnet/sdk:8.0 AS buildApp
WORKDIR /src
COPY . .
RUN dotnet publish "reto2_api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /application
COPY --from=buildApp /app ./
ENTRYPOINT ["dotnet", "reto2_api.dll"]