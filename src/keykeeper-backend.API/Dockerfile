FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/keykeeper-backend.API/keykeeper-backend.API.csproj", "src/keykeeper-backend.API/"]
COPY ["src/keykeeper-backend.Application/keykeeper-backend.Application.csproj", "src/keykeeper-backend.Application/"]
COPY ["src/keykeeper-backend.Domain/keykeeper-backend.Domain.csproj", "src/keykeeper-backend.Domain/"]
COPY ["src/keykeeper-backend.Infrastructure/keykeeper-backend.Infrastructure.csproj", "src/keykeeper-backend.Infrastructure/"]

RUN dotnet restore "src/keykeeper-backend.API/keykeeper-backend.API.csproj"
COPY . .
WORKDIR "/src/src/keykeeper-backend.API"
RUN dotnet build "keykeeper-backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "keykeeper-backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "keykeeper-backend.API.dll"]
