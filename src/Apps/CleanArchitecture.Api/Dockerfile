#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#ENV ASPNETCORE_URLS=https://+:5006;http://+:5005
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Apps/CleanArchitecture.Api/CleanArchitecture.Api.csproj", "src/Apps/CleanArchitecture.Api/"]
COPY ["src/Common/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "src/Common/CleanArchitecture.Api.Infrastructure/"]
COPY ["src/Common/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/Common/CleanArchitecture.Api.Application/"]
COPY ["src/Common/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/Common/CleanArchitecture.Api.Domain/"]
RUN dotnet restore "src/Apps/CleanArchitecture.Api/CleanArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/CleanArchitecture.Api"
RUN dotnet build "CleanArchitecture.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]