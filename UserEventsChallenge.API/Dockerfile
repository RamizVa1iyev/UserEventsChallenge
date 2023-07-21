#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserEventsChallenge.API/UserEventsChallenge.API.csproj", "UserEventsChallenge.API/"]
RUN dotnet restore "UserEventsChallenge.API/UserEventsChallenge.API.csproj"
COPY . .
WORKDIR "/src/UserEventsChallenge.API"
RUN dotnet build "UserEventsChallenge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserEventsChallenge.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserEventsChallenge.API.dll"]