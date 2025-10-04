# ======================
# Base image (runtime)
# ======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ======================
# Build stage
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution and csproj
COPY *.sln ./
COPY MarketplaceDemo/*.csproj ./MarketplaceDemo/

# restore
RUN dotnet restore

# copy rest of code
COPY . .

# publish
RUN dotnet publish MarketplaceDemo/MarketplaceDemo.csproj -c Release -o /app/publish

# ======================
# Final image
# ======================
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MarketplaceDemo.dll"]
