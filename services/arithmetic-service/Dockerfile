FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5004
ENV ASPNETCORE_URLS=http://+:5004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ArithmeticService.csproj", "."]
RUN dotnet restore "ArithmeticService.csproj"
COPY . .
RUN dotnet build "ArithmeticService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArithmeticService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArithmeticService.dll"]
