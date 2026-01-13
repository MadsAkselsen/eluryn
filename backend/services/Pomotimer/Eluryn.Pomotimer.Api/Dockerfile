# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Eluryn.Pomotimer.Api.csproj ./
RUN dotnet restore ./Eluryn.Pomotimer.Api.csproj

COPY . ./
RUN dotnet publish ./Eluryn.Pomotimer.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN useradd -m -u 10001 -s /usr/sbin/nologin eluryn
USER eluryn

COPY --from=build --chown=app:app /app/publish ./

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Eluryn.Pomotimer.Api.dll"]
