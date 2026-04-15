FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Enrolly.Notifications/Enrolly.Notifications/Enrolly.Notifications.csproj", "src/Services/Enrolly.Notifications/Enrolly.Notifications/"]
RUN dotnet restore "src/Services/Enrolly.Notifications/Enrolly.Notifications/Enrolly.Notifications.csproj"
COPY . .
WORKDIR "/src/src/Services/Enrolly.Notifications/Enrolly.Notifications"
RUN dotnet build "./Enrolly.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enrolly.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enrolly.Notifications.dll"]
