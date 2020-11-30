FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

RUN apt update && apt install libgdiplus -y

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

COPY . .

FROM build AS publish
WORKDIR /src/WebApi
RUN dotnet tool install --global dotnet-ef
RUN dotnet publish "WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi.dll"]
