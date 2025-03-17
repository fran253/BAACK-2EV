# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
# Copiar archivos de solución y de proyecto
COPY Back.sln ./
COPY Restaurante.csproj ./
RUN dotnet restore Restaurante.csproj
# Copiar el código fuente y compilar la aplicación
COPY . ./
RUN dotnet publish Restaurante.csproj -c Release -o /publish /p:TreatWarningsAsErrors=false
# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
# Copiar la aplicación compilada desde la etapa de build
COPY --from=build /publish .
# Exponer el puerto para la API
EXPOSE 8080
# Definir el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "reto2_api.dll"]