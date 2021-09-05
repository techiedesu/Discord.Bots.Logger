FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Discord.Bots.Logger/Discord.Bots.Logger.fsproj", "Discord.Bots.Logger/"]
RUN dotnet restore "Discord.Bots.Logger/Discord.Bots.Logger.fsproj"
COPY . .
WORKDIR "/src/Discord.Bots.Logger"
RUN dotnet build "Discord.Bots.Logger.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discord.Bots.Logger.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discord.Bots.Logger.dll"]
