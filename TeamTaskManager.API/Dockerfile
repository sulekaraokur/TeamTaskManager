#1.aşama : uygulamayı derleme (build)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["TeamTaskManager.API.csproj", "./"]
RUN dotnet restore "TeamTaskManager.API.csproj"
COPY . .
RUN dotnet publish "TeamTaskManager.API.csproj" -c Release -o /app/publish

#2.aşama: Çalıştırma (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "TeamTaskManager.API.dll"]

