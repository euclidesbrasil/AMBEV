FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ambev.General.Api.csproj", "./Ambev.General.Api/"]
RUN dotnet restore "./Ambev.General.Api/Ambev.General.Api.csproj"
COPY . .
WORKDIR "/src/Ambev.General.Api"
RUN dotnet publish "Ambev.General.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ambev.General.Api.dll"]