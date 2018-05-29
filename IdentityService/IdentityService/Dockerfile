FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 501

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY IdentityService.sln ./
COPY IdentityService/IdentityService.csproj IdentityService/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/IdentityService
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityService.dll"]
