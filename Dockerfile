# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia el archivo .csproj primero (mejor práctica para caching de Docker)
COPY ["ProductPerformanceCalculator/ProductPerformanceCalculator.csproj", "ProductPerformanceCalculator/"]
RUN dotnet restore "ProductPerformanceCalculator/ProductPerformanceCalculator.csproj"

# Copia el resto de archivos
COPY . .

# Publica la aplicación
RUN dotnet publish "ProductPerformanceCalculator/ProductPerformanceCalculator.csproj" -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductPerformanceCalculator.dll"]