# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Eluryn.Users.Api.csproj ./
RUN dotnet restore ./Eluryn.Users.Api.csproj

COPY . ./
RUN dotnet publish ./Eluryn.Users.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Optional but nice: run as non-root
RUN useradd -m -u 10001 -s /usr/sbin/nologin eluryn
USER eluryn

COPY --from=build --chown=app:app /app/publish ./

# ASP.NET Core defaults to 8080 in containers (good with compose mapping)
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Eluryn.Users.Api.dll"]
