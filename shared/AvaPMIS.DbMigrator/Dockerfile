#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["shared/AvaPMIS.DbMigrator/AvaPMIS.DbMigrator.csproj", "shared/AvaPMIS.DbMigrator/"]
COPY ["services/administration/src/AvaPMIS.Administration.Application.Contracts/AvaPMIS.Administration.Application.Contracts.csproj", "services/administration/src/AvaPMIS.Administration.Application.Contracts/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain.Shared/AvaPMIS.Administration.Domain.Shared.csproj", "services/administration/src/AvaPMIS.Administration.Domain.Shared/"]
COPY ["services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/AvaPMIS.Administration.EntityFrameworkCore.csproj", "services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain/AvaPMIS.Administration.Domain.csproj", "services/administration/src/AvaPMIS.Administration.Domain/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.Application.Contracts/AvaPMIS.IdentityService.Application.Contracts.csproj", "services/identity/src/AvaPMIS.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.Domain.Shared/AvaPMIS.IdentityService.Domain.Shared.csproj", "services/identity/src/AvaPMIS.IdentityService.Domain.Shared/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.EntityFrameworkCore/AvaPMIS.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/AvaPMIS.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/AvaPMIS.IdentityService.Domain/AvaPMIS.IdentityService.Domain.csproj", "services/identity/src/AvaPMIS.IdentityService.Domain/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Application.Contracts/AvaPMIS.SaaS.Application.Contracts.csproj", "services/saas/src/AvaPMIS.SaaS.Application.Contracts/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain.Shared/AvaPMIS.SaaS.Domain.Shared.csproj", "services/saas/src/AvaPMIS.SaaS.Domain.Shared/"]
COPY ["services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/AvaPMIS.SaaS.EntityFrameworkCore.csproj", "services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain/AvaPMIS.SaaS.Domain.csproj", "services/saas/src/AvaPMIS.SaaS.Domain/"]
COPY ["shared/AvaPMIS.Microservice.Shared/AvaPMIS.Microservice.Shared.csproj", "shared/AvaPMIS.Microservice.Shared/"]
COPY ["shared/AvaPMIS.Hosting.Shared/AvaPMIS.Hosting.Shared.csproj", "shared/AvaPMIS.Hosting.Shared/"]
RUN dotnet restore "shared/AvaPMIS.DbMigrator/AvaPMIS.DbMigrator.csproj"
COPY . .
WORKDIR "/src/shared/AvaPMIS.DbMigrator"
RUN dotnet build "AvaPMIS.DbMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvaPMIS.DbMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvaPMIS.DbMigrator.dll"]