#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Rova.Web/Rova.Web.csproj", "src/Rova.Web/"]
RUN dotnet restore "src/Rova.Web/Rova.Web.csproj"
COPY . .
WORKDIR "/src/src/Rova.Web"
RUN dotnet build "Rova.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rova.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rova.Web.dll"]