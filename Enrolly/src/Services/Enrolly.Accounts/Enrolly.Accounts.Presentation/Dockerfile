FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Enrolly.Accounts/Enrolly.Accounts.Presentation/Enrolly.Accounts.Presentation.csproj", "src/Services/Enrolly.Accounts/Enrolly.Accounts.Presentation/"]
COPY ["src/Services/Enrolly.Accounts/Enrolly.Accounts.Infrastructure/Enrolly.Accounts.Infrastructure.csproj", "src/Services/Enrolly.Accounts/Enrolly.Accounts.Infrastructure/"]
COPY ["src/Services/Enrolly.Accounts/Enrolly.Accounts.Domain/Enrolly.Accounts.Domain.csproj", "src/Services/Enrolly.Accounts/Enrolly.Accounts.Domain/"]
COPY ["src/Shared/Enrolly.Contracts/Enrolly.Contracts.csproj", "src/Shared/Enrolly.Contracts/"]
COPY ["src/Shared/Enrolly.Shared/Enrolly.Shared.csproj", "src/Shared/Enrolly.Shared/"]
COPY ["src/Services/Enrolly.Accounts/Enrolly.Accounts.Application/Enrolly.Accounts.Application.csproj", "src/Services/Enrolly.Accounts/Enrolly.Accounts.Application/"]
RUN dotnet restore "src/Services/Enrolly.Accounts/Enrolly.Accounts.Presentation/Enrolly.Accounts.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Services/Enrolly.Accounts/Enrolly.Accounts.Presentation"
RUN dotnet build "./Enrolly.Accounts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enrolly.Accounts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enrolly.Accounts.Presentation.dll"]
