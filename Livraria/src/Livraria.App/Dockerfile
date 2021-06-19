#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY ["src/Livraria.App/Livraria.App.csproj", "src/Livraria.App/"]
COPY ["src/Livraria.Infra.CrossCutting/Livraria.Infra.CrossCutting.csproj", "src/Livraria.Infra.CrossCutting/"]
COPY ["src/Livraria.Infra.Data/Livraria.Infra.Data.csproj", "src/Livraria.Infra.Data/"]
COPY ["src/Livraria.Domain/Livraria.Domain.csproj", "src/Livraria.Domain/"]
RUN dotnet restore "src/Livraria.App/Livraria.App.csproj"
COPY . .
WORKDIR "/src/Livraria.App"
RUN dotnet build "Livraria.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Livraria.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Livraria.App.dll"]
