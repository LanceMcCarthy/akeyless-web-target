FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
ARG BUILDPLATFORM
RUN echo "TARGETARCH     => $TARGETARCH"
RUN echo "BUILDPLATFORM  => $BUILDPLATFORM"
WORKDIR '/src/SecretsMocker/SecretsMocker'
COPY . .
RUN dotnet publish './SecretsMocker.csproj'-c Release -o /app/publish /p:UseAppHost=false --self-contained false --arch $TARGETARCH

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./SecretsMocker"]
