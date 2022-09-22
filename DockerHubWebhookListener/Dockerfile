FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerHubWebhookListener/DockerHubWebhookListener.csproj", "DockerHubWebhookListener/"]
RUN dotnet restore "DockerHubWebhookListener/DockerHubWebhookListener.csproj"
COPY . .
WORKDIR "/src/DockerHubWebhookListener"
RUN dotnet build "DockerHubWebhookListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerHubWebhookListener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerHubWebhookListener.dll"]
