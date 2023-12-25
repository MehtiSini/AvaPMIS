#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["apps/AvaPMIS.AuthServer/AvaPMIS.AuthServer.csproj", "apps/AvaPMIS.AuthServer/"]
COPY ["services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/AvaPMIS.Administration.EntityFrameworkCore.csproj", "services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain/AvaPMIS.Administration.Domain.csproj", "services/administration/src/AvaPMIS.Administration.Domain/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain.Shared/AvaPMIS.Administration.Domain.Shared.csproj", "services/administration/src/AvaPMIS.Administration.Domain.Shared/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.EntityFrameworkCore/AvaPMIS.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/AvaPMIS.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.Domain/AvaPMIS.IdentityService.Domain.csproj", "services/identity/src/AvaPMIS.IdentityService.Domain/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.Domain.Shared/AvaPMIS.IdentityService.Domain.Shared.csproj", "services/identity/src/AvaPMIS.IdentityService.Domain.Shared/"]
COPY ["services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/AvaPMIS.SaaS.EntityFrameworkCore.csproj", "services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain/AvaPMIS.SaaS.Domain.csproj", "services/saas/src/AvaPMIS.SaaS.Domain/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain.Shared/AvaPMIS.SaaS.Domain.Shared.csproj", "services/saas/src/AvaPMIS.SaaS.Domain.Shared/"]
COPY ["shared/AvaPMIS.Microservice.Shared/AvaPMIS.Microservice.Shared.csproj", "shared/AvaPMIS.Microservice.Shared/"]
COPY ["shared/AvaPMIS.Hosting.Shared/AvaPMIS.Hosting.Shared.csproj", "shared/AvaPMIS.Hosting.Shared/"]
RUN dotnet restore "apps/AvaPMIS.AuthServer/AvaPMIS.AuthServer.csproj"
COPY . .
WORKDIR "/src/apps/AvaPMIS.AuthServer"
RUN dotnet build "AvaPMIS.AuthServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvaPMIS.AuthServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvaPMIS.AuthServer.dll"]