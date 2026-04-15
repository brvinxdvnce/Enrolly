FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Enrolly.Documents/Enrolly.Documents.Presentation/Enrolly.Documents.Presentation.csproj", "src/Services/Enrolly.Documents/Enrolly.Documents.Presentation/"]
COPY ["src/Services/Enrolly.Documents/Enrolly.Documents.Infrastructure/Enrolly.Documents.Infrastructure.csproj", "src/Services/Enrolly.Documents/Enrolly.Documents.Infrastructure/"]
COPY ["src/Services/Enrolly.Documents/Enrolly.Documents.Domain/Enrolly.Documents.Domain.csproj", "src/Services/Enrolly.Documents/Enrolly.Documents.Domain/"]
COPY ["src/Services/Enrolly.Documents/Enrolly.Documents.Application/Enrolly.Documents.Application.csproj", "src/Services/Enrolly.Documents/Enrolly.Documents.Application/"]
RUN dotnet restore "src/Services/Enrolly.Documents/Enrolly.Documents.Presentation/Enrolly.Documents.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Services/Enrolly.Documents/Enrolly.Documents.Presentation"
RUN dotnet build "./Enrolly.Documents.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enrolly.Documents.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enrolly.Documents.Presentation.dll"]
