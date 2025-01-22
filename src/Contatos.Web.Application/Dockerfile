#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Contatos.Web.Application/Contatos.Web.Application.csproj", "src/Contatos.Web.Application/"]
# COPY Contatos.Web.Domain.csproj /src/Contatos.Web.Domain/
# COPY Contatos.Web.Infrastructure.CrossCutting.csproj Contatos.Web.Infrastructure.CrossCutting/
# COPY Contatos.Web.Infrastructure.Data.csproj src/Contatos.Web.Infrastructure.Data/
# COPY Contatos.Web.Service.csproj src/Contatos.Web.Service/
# COPY Contatos.Web.Shared.csproj src/Contatos.Web.Shared/
RUN dotnet restore "./src/Contatos.Web.Application/./Contatos.Web.Application.csproj"
COPY . .
WORKDIR "/src/src/Contatos.Web.Application"
RUN dotnet build "./Contatos.Web.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Contatos.Web.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.Web.Application.dll"]