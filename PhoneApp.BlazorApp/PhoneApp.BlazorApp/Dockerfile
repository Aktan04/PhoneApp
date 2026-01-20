FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PhoneApp.BlazorApp/PhoneApp.BlazorApp/PhoneApp.BlazorApp.csproj", "PhoneApp.BlazorApp/PhoneApp.BlazorApp/"]
COPY ["PhoneApp.BlazorApp/PhoneApp.BlazorApp.Client/PhoneApp.BlazorApp.Client.csproj", "PhoneApp.BlazorApp/PhoneApp.BlazorApp.Client/"]
RUN dotnet restore "PhoneApp.BlazorApp/PhoneApp.BlazorApp/PhoneApp.BlazorApp.csproj"
COPY . .
WORKDIR "/src/PhoneApp.BlazorApp/PhoneApp.BlazorApp"
RUN dotnet build "PhoneApp.BlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PhoneApp.BlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneApp.BlazorApp.dll"]
