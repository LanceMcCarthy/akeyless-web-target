FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR '/src/SecretsMocker/SecretsMocker'
COPY . .
RUN dotnet restore './SecretsMocker.csproj'
RUN dotnet build './SecretsMocker.csproj' -c Release -o /app/build --no-restore

FROM build AS publish

RUN dotnet publish './SecretsMocker.csproj' --no-restore -c Release -o /app/publish /p:UseAppHost=false --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretsMocker.dll"]
