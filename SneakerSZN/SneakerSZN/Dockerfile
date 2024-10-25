#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["SneakerSZN/SneakerSZN.csproj", "SneakerSZN/"]
#RUN dotnet restore "./SneakerSZN/SneakerSZN.csproj"
#COPY . .
#WORKDIR "/src/SneakerSZN"
#RUN dotnet build "./SneakerSZN.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./SneakerSZN.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SneakerSZN.dll"]

# Start from the base ASP.NET image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage using the SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution file and restore dependencies
COPY ["SneakerSZN.sln", "."]
COPY ["SneakerSZN/SneakerSZN.csproj", "SneakerSZN/"]
COPY ["SneakerSZN_BLL/SneakerSZN_BLL.csproj", "SneakerSZN_BLL/"]
COPY ["SneakerSZN_DAL/SneakerSZN_DAL.csproj", "SneakerSZN_DAL/"]
RUN dotnet restore

# Copy all other files and build the project
COPY . .
WORKDIR "/src/SneakerSZN"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SneakerSZN.dll"]
