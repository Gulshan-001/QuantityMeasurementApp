# Stage 1: Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files and restore dependencies
# This ensures that restoration is cached unless a project file changes
COPY ["QuantityMeasurementWebAPI/QuantityMeasurementConsole.csproj", "QuantityMeasurementWebAPI/"]
COPY ["QuantityMeasurementBusinessLayer/QuantityMeasurementBusinessLayer.csproj", "QuantityMeasurementBusinessLayer/"]
COPY ["QuantityMeasurementModel/QuantityMeasurementModelLayer.csproj", "QuantityMeasurementModel/"]
COPY ["QuantityMeasurementRepository/QuantityMeasurementRepositoryLayer.csproj", "QuantityMeasurementRepository/"]

RUN dotnet restore "QuantityMeasurementWebAPI/QuantityMeasurementConsole.csproj"

# Copy the rest of the source code
COPY . .

# Build the app
WORKDIR "/src/QuantityMeasurementWebAPI"
RUN dotnet build "QuantityMeasurementConsole.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "QuantityMeasurementConsole.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuantityMeasurementConsole.dll"]
