FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "./"]
COPY ["STW.SubmissionApi.Presentation/STW.SubmissionApi.Presentation.csproj", "STW.SubmissionApi.Presentation/"]
RUN dotnet restore "STW.SubmissionApi.Presentation/STW.SubmissionApi.Presentation.csproj"
COPY . .
WORKDIR "/src/STW.SubmissionApi.Presentation"
RUN dotnet build "STW.SubmissionApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "STW.SubmissionApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "STW.SubmissionApi.Presentation.dll"]
