
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["ProductPerformanceCalculator/ProductPerformanceCalculator.csproj", "ProductPerformanceCalculator/"]
RUN dotnet restore "ProductPerformanceCalculator/ProductPerformanceCalculator.csproj"


COPY . .


RUN dotnet publish "ProductPerformanceCalculator/ProductPerformanceCalculator.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductPerformanceCalculator.dll"]