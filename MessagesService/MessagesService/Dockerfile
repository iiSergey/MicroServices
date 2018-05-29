FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MessagesService/MessagesService.csproj MessagesService/
RUN dotnet restore MessagesService/MessagesService.csproj
COPY . .
WORKDIR /src/MessagesService
RUN dotnet build MessagesService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MessagesService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MessagesService.dll"]
