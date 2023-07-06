FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Battleships/Battleships.csproj", "src/Battleships/"]
COPY ["src/Battleships.Common/Battleships.Common.csproj", "src/Battleships.Common/"]
COPY ["src/Battleships.Models/Battleships.Models.csproj", "src/Battleships.Models/"]
COPY ["src/Battleships.UI/Battleships.UI.csproj", "src/Battleships.UI/"]
RUN dotnet restore "src/Battleships/Battleships.csproj"
COPY . .
WORKDIR "/src/src/Battleships"
RUN dotnet build "Battleships.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Battleships.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Battleships.dll"]