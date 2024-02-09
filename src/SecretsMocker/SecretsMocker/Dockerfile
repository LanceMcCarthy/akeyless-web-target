FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR "/src/SecretsMocker/SecretsMocker"
COPY . .
RUN --mount=type=secret,id=telerik_key \
  dotnet nuget update source "Telerik_Feed" -s "https://nuget.telerik.com/v3/index.json" -u "api-key" -p $(cat /run/secrets/telerik_key) --configfile "./nuget.config" --store-password-in-clear-text
RUN dotnet restore "./SecretsMocker.csproj" --configfile "./nuget.config"
RUN dotnet build "./SecretsMocker.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "./SecretsMocker.csproj" --no-restore -c Release -o /app/publish /p:UseAppHost=false --self-contained false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretsMocker.dll"]
