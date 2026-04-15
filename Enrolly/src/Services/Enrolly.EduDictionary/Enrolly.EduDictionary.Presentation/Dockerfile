FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Presentation/Enrolly.EduDictionary.Presentation.csproj", "src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Presentation/"]
COPY ["src/Services/Enrolly.EduDictionary/DictionaryWorker/DictionaryWorker.csproj", "src/Services/Enrolly.EduDictionary/DictionaryWorker/"]
COPY ["src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Domain/Enrolly.EduDictionary.Domain.csproj", "src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Domain/"]
COPY ["src/Shared/Enrolly.Shared/Enrolly.Shared.csproj", "src/Shared/Enrolly.Shared/"]
COPY ["src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Application/Enrolly.EduDictionary.Application.csproj", "src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Application/"]
RUN dotnet restore "src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Presentation/Enrolly.EduDictionary.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.Presentation"
RUN dotnet build "./Enrolly.EduDictionary.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enrolly.EduDictionary.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enrolly.EduDictionary.Presentation.dll"]
