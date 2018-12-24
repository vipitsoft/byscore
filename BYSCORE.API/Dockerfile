FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 9999

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY BYSCORE.API/BYSCORE.API.csproj BYSCORE.API/
COPY BYSCORE.Dao/BYSCORE.Dao.csproj BYSCORE.Dao/
COPY BYSCORE.Entity/BYSCORE.Entity.csproj BYSCORE.Entity/
COPY BYSCORE.Common/BYSCORE.Common.csproj BYSCORE.Common/
RUN dotnet restore BYSCORE.API/BYSCORE.API.csproj
COPY . .
WORKDIR /src/BYSCORE.API
RUN dotnet build BYSCORE.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BYSCORE.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BYSCORE.API.dll"]
