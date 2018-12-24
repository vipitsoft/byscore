FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY BYSCORE.UI/BYSCORE.UI.csproj BYSCORE.UI/
COPY BYSCORE.Common/BYSCORE.Common.csproj BYSCORE.Common/
COPY BYSCORE.Entity/BYSCORE.Entity.csproj BYSCORE.Entity/
RUN dotnet restore BYSCORE.UI/BYSCORE.UI.csproj
COPY . .
WORKDIR /src/BYSCORE.UI
RUN dotnet build BYSCORE.UI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BYSCORE.UI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BYSCORE.UI.dll"]
