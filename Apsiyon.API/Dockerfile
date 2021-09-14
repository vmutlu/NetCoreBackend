FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Apsiyon.API/Apsiyon.API.csproj", "Apsiyon.API/"]
COPY ["Apsiyon.Business/Apsiyon.Business.csproj", "Apsiyon.Business/"]
COPY ["Apsiyon.Entities/Apsiyon.Entities.csproj", "Apsiyon.Entities/"]
COPY ["Apsiyon.DataAccess/Apsiyon.DataAccess.csproj", "Apsiyon.DataAccess/"]
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