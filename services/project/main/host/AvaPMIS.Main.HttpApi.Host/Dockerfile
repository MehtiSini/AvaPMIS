#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/identity/host/AvaPMIS.Main.HttpApi.Host/AvaPMIS.Main.HttpApi.Host.csproj", "services/identity/host/AvaPMIS.Main.HttpApi.Host/"]
COPY ["shared/AvaPMIS.Hosting.Shared/AvaPMIS.Hosting.Shared.csproj", "shared/AvaPMIS.Hosting.Shared/"]
COPY ["services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/AvaPMIS.Administration.EntityFrameworkCore.csproj", "services/administration/src/AvaPMIS.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain/AvaPMIS.Administration.Domain.csproj", "services/administration/src/AvaPMIS.Administration.Domain/"]
COPY ["services/administration/src/AvaPMIS.Administration.Domain.Shared/AvaPMIS.Administration.Domain.Shared.csproj", "services/administration/src/AvaPMIS.Administration.Domain.Shared/"]
COPY ["services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/AvaPMIS.SaaS.EntityFrameworkCore.csproj", "services/saas/src/AvaPMIS.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain/AvaPMIS.SaaS.Domain.csproj", "services/saas/src/AvaPMIS.SaaS.Domain/"]
COPY ["services/saas/src/AvaPMIS.SaaS.Domain.Shared/AvaPMIS.SaaS.Domain.Shared.csproj", "services/saas/src/AvaPMIS.SaaS.Domain.Shared/"]
COPY ["services/identity/src/AvaPMIS.Main.HttpApi/AvaPMIS.Main.HttpApi.csproj", "services/identity/src/AvaPMIS.Main.HttpApi/"]
COPY ["services/identity/src/AvaPMIS.Main.Application.Contracts/AvaPMIS.Main.Application.Contracts.csproj", "services/identity/src/AvaPMIS.Main.Application.Contracts/"]
COPY ["services/identity/src/AvaPMIS.Main.Domain.Shared/AvaPMIS.Main.Domain.Shared.csproj", "services/identity/src/AvaPMIS.Main.Domain.Shared/"]
COPY ["services/identity/src/AvaPMIS.Main.Application/AvaPMIS.Main.Application.csproj", "services/identity/src/AvaPMIS.Main.Application/"]
COPY ["services/identity/src/AvaPMIS.Main.Domain/AvaPMIS.Main.Domain.csproj", "services/identity/src/AvaPMIS.Main.Domain/"]
COPY ["services/identity/src/AvaPMIS.Main.EntityFrameworkCore/AvaPMIS.Main.EntityFrameworkCore.csproj", "services/identity/src/AvaPMIS.Main.EntityFrameworkCore/"]
RUN dotnet restore "services/identity/host/AvaPMIS.Main.HttpApi.Host/AvaPMIS.Main.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/identity/host/AvaPMIS.Main.HttpApi.Host"
RUN dotnet build "AvaPMIS.Main.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvaPMIS.Main.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvaPMIS.Main.HttpApi.Host.dll"]