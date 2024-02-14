FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
ARG BUILDPLATFORM
WORKDIR '/src/SecretsMocker/SecretsMocker'
COPY . .
RUN dotnet restore -a $TARGETARCH
RUN dotnet build -a $TARGETARCH
RUN dotnet publish './SecretsMocker.csproj' -o /app/publish -a $TARGETARCH

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 as final
ARG BUILDPLATFORM
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./SecretsMocker"]
