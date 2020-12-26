#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Apps/Client.WorkerService/Client.WorkerService.csproj", "src/Apps/Client.WorkerService/"]
RUN dotnet restore "src/Apps/Client.WorkerService/Client.WorkerService.csproj"
COPY . .
WORKDIR "/src/src/Apps/Client.WorkerService"
RUN dotnet build "Client.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.WorkerService.dll"]