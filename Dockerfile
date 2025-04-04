# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar los archivos de solución y proyecto para restaurar dependencias
COPY *.sln ./
COPY ProductPerformanceCalculator/*.csproj ./ProductPerformanceCalculator/
RUN dotnet restore ProductPerformanceCalculator/*.csproj

# Copiar el código fuente y compilar
COPY . .
WORKDIR /app/ProductPerformanceCalculator
RUN dotnet publish -c Release -o /out

# Etapa 2: Ejecución (Imagen más liviana)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .

# Exponer el puerto en el que corre la API
EXPOSE 5000

# Definir el comando de inicio
ENTRYPOINT ["dotnet", "ProductPerformanceCalculator.dll"]