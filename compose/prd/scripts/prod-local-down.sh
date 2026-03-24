#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PRD_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"

DB_COMPOSE_FILE="$PRD_DIR/compose.db.local.yml"
APP_COMPOSE_FILE="$PRD_DIR/compose.prd.local.yml"

echo "Stopping application stack..."
docker compose -f "$APP_COMPOSE_FILE" down --remove-orphans

echo "Stopping database stack and removing volume..."
docker compose -f "$DB_COMPOSE_FILE" down -v --remove-orphans