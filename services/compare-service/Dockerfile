FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002
ENV ASPNETCORE_URLS=http://+:5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CompareService.csproj", "."]
RUN dotnet restore "CompareService.csproj"
COPY . .
RUN dotnet build "CompareService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompareService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompareService.dll"]
