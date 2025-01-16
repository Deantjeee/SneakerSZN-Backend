#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SneakerSZN/SneakerSZN.csproj", "SneakerSZN/"]
COPY ["SneakerSZN_DAL/SneakerSZN_DAL.csproj", "SneakerSZN_DAL/"]
COPY ["SneakerSZN_BLL/SneakerSZN_BLL.csproj", "SneakerSZN_BLL/"]
RUN dotnet restore "./SneakerSZN/SneakerSZN.csproj"
COPY . .
WORKDIR "/src/SneakerSZN"
RUN dotnet build "./SneakerSZN.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SneakerSZN.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SneakerSZN.dll"]