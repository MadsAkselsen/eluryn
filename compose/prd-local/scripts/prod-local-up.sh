#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PRD_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"
ROOT_DIR="$(cd "$PRD_DIR/../.." && pwd)"

DB_COMPOSE_FILE="$PRD_DIR/compose.db.local.yml"
APP_COMPOSE_FILE="$PRD_DIR/compose.prd.local.yml"
INIT_SQL="$PRD_DIR/local.db.init.sql"

USERS_MIGRATIONS="$ROOT_DIR/backend/services/Users/Eluryn.Users.Api/migrations"
POMOTIMER_MIGRATIONS="$ROOT_DIR/backend/services/Pomotimer/Eluryn.Pomotimer.Api/migrations"

DB_CONTAINER_NAME="eluryn-db-local"
DB_NETWORK_NAME="eluryn-db-shared"

echo "Starting database stack..."
docker compose -f "$DB_COMPOSE_FILE" up -d

echo "Waiting for database to become healthy..."
until [ "$(docker inspect -f '{{.State.Health.Status}}' "$DB_CONTAINER_NAME")" = "healthy" ]; do
  sleep 2
done

echo "Running DB init script..."
docker run --rm \
  --network "$DB_NETWORK_NAME" \
  -e PGPASSWORD=postgres \
  -v "$INIT_SQL:/init.sql:ro" \
  postgres:16 \
  psql -h db -U postgres -d eluryn -v ON_ERROR_STOP=1 -f /init.sql

echo "Running Flyway migrations for users..."
docker run --rm \
  --network "$DB_NETWORK_NAME" \
  -v "$USERS_MIGRATIONS:/flyway/sql:ro" \
  flyway/flyway:12.0.0 \
  -url=jdbc:postgresql://db:5432/eluryn \
  -user=users_svc \
  -password=users_pw \
  -connectRetries=30 \
  -schemas=users \
  -locations=filesystem:/flyway/sql \
  migrate

echo "Running Flyway migrations for pomotimer..."
docker run --rm \
  --network "$DB_NETWORK_NAME" \
  -v "$POMOTIMER_MIGRATIONS:/flyway/sql:ro" \
  flyway/flyway:12.0.0 \
  -url=jdbc:postgresql://db:5432/eluryn \
  -user=pomotimer_svc \
  -password=pomo_pw \
  -connectRetries=30 \
  -schemas=pomotimer \
  -locations=filesystem:/flyway/sql \
  migrate

echo "Starting application stack..."
docker compose -f "$APP_COMPOSE_FILE" up -d --build

echo ""
echo "Prod-local environment is up."
echo "Frontend: https://app.localhost"
echo "API:      https://api.localhost"
echo "Traefik:  http://localhost:8080"