#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["API2PSSale/API2PSSale.csproj", "API2PSSale/"]
RUN dotnet restore "API2PSSale/API2PSSale.csproj"
COPY . .
WORKDIR "/src/API2PSSale"
RUN dotnet build "API2PSSale.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API2PSSale.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API2PSSale.dll"]