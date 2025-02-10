#Setup local db (Sql server)
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04



docker compose up -d


//Para el reto 2

docker run --name reto2_api \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=TuContraseñaSegura123!" \
  -p 1433:1433 \
  -v reto2_data:/var/opt/mssql \
  -d mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

