FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BaltaIOChallenge.Api/BaltaIOChallenge.Api.csproj", "BaltaIOChallenge.Api/"]
RUN dotnet restore "BaltaIOChallenge.Api/BaltaIOChallenge.Api.csproj"
COPY . .
WORKDIR "/src/BaltaIOChallenge.Api"
RUN dotnet build "BaltaIOChallenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BaltaIOChallenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaltaIOChallenge.Api.dll"]
