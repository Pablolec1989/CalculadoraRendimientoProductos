FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR webapp

EXPOSE 80
EXPOSE 5000

COPY ./*.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release - out

FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT ["dotnet", "ProductPerformanceCalculator.dll"]