#!/usr/bin/env sh
set -eu

APP_DIR=${APP_DIR:-/opt/eluryn}
COMPOSE_FILE=${COMPOSE_FILE:-compose.prod.yml}
ENV_FILE=${ENV_FILE:-.env}

cd "$APP_DIR"

docker compose --env-file "$ENV_FILE" -f "$COMPOSE_FILE" pull

docker compose --env-file "$ENV_FILE" -f "$COMPOSE_FILE" --profile migrations run --rm flyway-users
docker compose --env-file "$ENV_FILE" -f "$COMPOSE_FILE" --profile migrations run --rm flyway-pomotimer

docker compose --env-file "$ENV_FILE" -f "$COMPOSE_FILE" up -d --remove-orphans

docker compose --env-file "$ENV_FILE" -f "$COMPOSE_FILE" ps
docker image prune -f --filter "until=168h"
