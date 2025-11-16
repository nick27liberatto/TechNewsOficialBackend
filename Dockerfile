FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TechNewsOficialBackend.Api/TechNewsOficialBackend.Api.csproj", "TechNewsOficialBackend.Api/"]
RUN dotnet restore "TechNewsOficialBackend.Api/TechNewsOficialBackend.Api.csproj"

WORKDIR "/src/TechNewsOficialBackend.Api"
COPY . .

RUN dotnet build "TechNewsOficialBackend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechNewsOficialBackend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechNewsOficialBackend.Api.dll"]