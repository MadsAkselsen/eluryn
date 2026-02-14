# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Eluryn.Users.Api.csproj ./
RUN dotnet restore ./Eluryn.Users.Api.csproj

COPY . ./
RUN dotnet publish ./Eluryn.Users.Api.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Install curl (for health checks)
RUN apt-get update \
  && apt-get install -y --no-install-recommends curl \
  && rm -rf /var/lib/apt/lists/*

# Optional but nice: run as non-root
RUN useradd -m -u 10001 -s /usr/sbin/nologin eluryn
USER eluryn

COPY --from=build /app/publish ./
EXPOSE 8080

ENTRYPOINT ["dotnet", "Eluryn.Users.Api.dll"]
