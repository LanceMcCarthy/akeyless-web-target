FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
ARG BUILDPLATFORM

WORKDIR '/src/SecretsMocker/SecretsMocker'
COPY . .
RUN dotnet restore './SecretsMocker.csproj'
RUN dotnet build './SecretsMocker.csproj' -c Release -o /app/build --no-restore

FROM build AS publish
ARG TARGETARCH
ARG BUILDPLATFORM
RUN echo "TARGETARCH     => $TARGETARCH"
RUN echo "BUILDPLATFORM  => $BUILDPLATFORM"
RUN dotnet publish './SecretsMocker.csproj' --no-restore -c Release -o /app/publish /p:UseAppHost=false --self-contained false -a "${TARGETARCH}"
 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretsMocker.dll"]
