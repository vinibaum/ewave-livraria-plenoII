#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Livraria.App/Livraria.App.csproj", "Livraria.App/"]
RUN dotnet restore "Livraria.App/Livraria.App.csproj"
COPY . .
WORKDIR "/src/Livraria.App"
RUN dotnet build "Livraria.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Livraria.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Livraria.App.dll"]