FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Enrolly.Admissions/Enrolly.Admissions.Presentation/Enrolly.Admissions.Presentation.csproj", "src/Services/Enrolly.Admissions/Enrolly.Admissions.Presentation/"]
COPY ["src/Services/Enrolly.Admissions/Enrolly.Admissions.Domain/Enrolly.Admissions.Domain.csproj", "src/Services/Enrolly.Admissions/Enrolly.Admissions.Domain/"]
COPY ["src/Services/Enrolly.Admissions/Enrolly.Admissions.Application/Enrolly.Admissions.Application.csproj", "src/Services/Enrolly.Admissions/Enrolly.Admissions.Application/"]
RUN dotnet restore "src/Services/Enrolly.Admissions/Enrolly.Admissions.Presentation/Enrolly.Admissions.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Services/Enrolly.Admissions/Enrolly.Admissions.Presentation"
RUN dotnet build "./Enrolly.Admissions.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enrolly.Admissions.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enrolly.Admissions.Presentation.dll"]
