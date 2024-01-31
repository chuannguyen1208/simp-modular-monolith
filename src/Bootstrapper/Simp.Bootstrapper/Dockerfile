#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Bootstrapper/Simp.Bootstrapper/Simp.Bootstrapper.csproj", "src/Bootstrapper/Simp.Bootstrapper/"]
COPY ["src/Modules/Blogs/Simp.Modules.Blogs.Api/Simp.Modules.Blogs.Api.csproj", "src/Modules/Blogs/Simp.Modules.Blogs.Api/"]
COPY ["src/Shared/Simp.Shared.Abstractions/Simp.Shared.Abstractions.csproj", "src/Shared/Simp.Shared.Abstractions/"]
COPY ["src/Modules/Blogs/Simp.Modules.Blogs.Infrastructure/Simp.Modules.Blogs.Infrastructure.csproj", "src/Modules/Blogs/Simp.Modules.Blogs.Infrastructure/"]
COPY ["src/Shared/Simp.Shared.Infrastructure/Simp.Shared.Infrastructure.csproj", "src/Shared/Simp.Shared.Infrastructure/"]
COPY ["src/Modules/Blogs/Simp.Modules.Blogs.Domain/Simp.Modules.Blogs.Domain.csproj", "src/Modules/Blogs/Simp.Modules.Blogs.Domain/"]
COPY ["src/Modules/Blogs/Simp.Modules.Blogs.UseCases/Simp.Modules.Blogs.UseCases.csproj", "src/Modules/Blogs/Simp.Modules.Blogs.UseCases/"]
COPY ["src/Modules/Blogs/Simp.Modules.Blogs.Contracts/Simp.Modules.Blogs.Contracts.csproj", "src/Modules/Blogs/Simp.Modules.Blogs.Contracts/"]
COPY ["src/Modules/Cshops/Simp.Modules.Cshops.Api/Simp.Modules.Cshops.Api.csproj", "src/Modules/Cshops/Simp.Modules.Cshops.Api/"]
COPY ["src/Modules/Cshops/Simp.Modules.Cshops.Infrastructure/Simp.Modules.Cshops.Infrastructure.csproj", "src/Modules/Cshops/Simp.Modules.Cshops.Infrastructure/"]
COPY ["src/Modules/Cshops/Simp.Modules.Cshops.UseCases/Simp.Modules.Cshops.UseCases.csproj", "src/Modules/Cshops/Simp.Modules.Cshops.UseCases/"]
COPY ["src/Modules/Cshops/Simp.Modules.Cshop.Domain/Simp.Modules.Cshop.Domain.csproj", "src/Modules/Cshops/Simp.Modules.Cshop.Domain/"]
RUN dotnet restore "./src/Bootstrapper/Simp.Bootstrapper/Simp.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Simp.Bootstrapper"
RUN dotnet build "./Simp.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Simp.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simp.Bootstrapper.dll"]