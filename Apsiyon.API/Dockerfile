#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Apsiyon.API/Apsiyon.API.csproj", "Apsiyon.API/"]
RUN dotnet restore "Apsiyon.API/Apsiyon.API.csproj"
COPY . .
WORKDIR "/src/Apsiyon.API"
RUN dotnet build "Apsiyon.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apsiyon.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apsiyon.API.dll"]