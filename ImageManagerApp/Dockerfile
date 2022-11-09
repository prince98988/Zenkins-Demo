#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ImageManagerApp/ImageManagerApp.csproj", "ImageManagerApp/"]
RUN dotnet restore "ImageManagerApp/ImageManagerApp.csproj"
COPY . .
WORKDIR "/src/ImageManagerApp"
RUN dotnet build "ImageManagerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageManagerApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageManagerApp.dll"]