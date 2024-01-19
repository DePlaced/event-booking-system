#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BigDSignWebApp/BigDSignWebApp.csproj", "BigDSignWebApp/"]
RUN dotnet restore "BigDSignWebApp/BigDSignWebApp.csproj"
COPY . .
WORKDIR "/src/BigDSignWebApp"
RUN dotnet build "BigDSignWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BigDSignWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BigDSignWebApp.dll"]