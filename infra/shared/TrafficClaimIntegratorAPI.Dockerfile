# Use .NET 9 Preview base runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app
EXPOSE 8080

# SDK build image
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy just the csproj
COPY TrafficClaimIntegratorAPI.csproj ./

# Restore it from current folder
RUN dotnet restore TrafficClaimIntegratorAPI.csproj

# Copy everything (source code)
COPY . .

RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrafficClaimIntegratorAPI.dll"]
