FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src


COPY ["AgroSolutions.History.API/AgroSolutions.History.API.csproj", "AgroSolutions.History.API/"]
COPY ["AgroSolutions.History.Application/AgroSolutions.History.Application.csproj", "AgroSolutions.History.Application/"]
COPY ["AgroSolutions.History.Domain/AgroSolutions.History.Domain.csproj", "AgroSolutions.History.Domain/"]
COPY ["AgroSolutions.History.Infrastructure/AgroSolutions.History.Infrastructure.csproj", "AgroSolutions.History.Infrastructure/"]

RUN dotnet restore "AgroSolutions.History.API/AgroSolutions.History.API.csproj"

COPY . .

WORKDIR "/src/AgroSolutions.History.API"
RUN dotnet build "AgroSolutions.History.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgroSolutions.History.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "AgroSolutions.History.API.dll"]