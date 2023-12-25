#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/host/AvaPMIS.SaaS.HttpApi.Host/AvaPMIS.SaaS.HttpApi.Host.csproj", "services/saas/host/AvaPMIS.SaaS.HttpApi.Host/"]
COPY ["shared/AvaPMIS.Hosting.Shared/AvaPMIS.Hosting.Shared.csproj", "shared/AvaPMIS.Hosting.Shared/"]
COPY ["services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/AvaPMIS.Administration.EntityFrameworkCore.csproj", "services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain/AvaPMIS.Administration.Domain.csproj", "services/administration/src/AvaPMIS.Administration.Domain/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain.Shared/AvaPMIS.Administration.Domain.Shared.csproj", "services/administration/src/AvaPMIS.Administration.Domain.Shared/"]
COPY ["services/saas/src/AvaPMIS.SaaS.HttpApi/AvaPMIS.SaaS.HttpApi.csproj", "services/saas/src/AvaPMIS.SaaS.HttpApi/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Application.Contracts/AvaPMIS.SaaS.Application.Contracts.csproj", "services/saas/src/AvaPMIS.SaaS.Application.Contracts/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain.Shared/AvaPMIS.SaaS.Domain.Shared.csproj", "services/saas/src/AvaPMIS.SaaS.Domain.Shared/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Application/AvaPMIS.SaaS.Application.csproj", "services/saas/src/AvaPMIS.SaaS.Application/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain/AvaPMIS.SaaS.Domain.csproj", "services/saas/src/AvaPMIS.SaaS.Domain/"]
COPY ["services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/AvaPMIS.SaaS.EntityFrameworkCore.csproj", "services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/"]
RUN dotnet restore "services/saas/host/AvaPMIS.SaaS.HttpApi.Host/AvaPMIS.SaaS.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/saas/host/AvaPMIS.SaaS.HttpApi.Host"
RUN dotnet build "AvaPMIS.SaaS.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvaPMIS.SaaS.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvaPMIS.SaaS.HttpApi.Host.dll"]