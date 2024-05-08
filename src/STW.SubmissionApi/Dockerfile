FROM mcr.microsoft.com/dotnet/sdk:8.0-preview as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY . /src
WORKDIR /src
RUN cd /src
RUN dotnet build "STW.SubmissionApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "STW.SubmissionApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "STW.SubmissionApi.dll"]