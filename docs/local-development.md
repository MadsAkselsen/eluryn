# Local development

A local development environment is set up using Docker Compose.

## Run, stop, restart

#### Start everything (with frontend watch mode)

```bash
docker compose -f compose/compose.local.yml watch
```

#### Stop all containers (clean shutdown)

```bash
docker compose -f compose/compose.local.yml down
```

#### Restart the frontend (keeps watch mode):

```bash
docker compose -f compose/compose.local.yml restart frontend
```

#### Restart a backend service:

```bash
docker compose -f compose/compose.local.yml restart users-api
docker compose -f compose/compose.local.yml restart pomotimer-api
```

#### Rebuild everything (without deleting data) - useful if you changed the dockerfile

```bash
docker compose -f compose/compose.local.yml build --no-cache
docker compose -f compose/compose.local.yml watch
```

#### Full reset (wipe DBs and rebuild everything)

```bash
docker compose -f compose/compose.local.yml down -v
docker compose -f compose/compose.local.yml build --no-cache
docker compose -f compose/compose.local.yml watch
```

## Debugging and logs

#### Show container status:

```bash
docker compose -f compose/compose.local.yml ps
```

#### View logs for a specific service:

```bash
docker compose -f compose/compose.local.yml logs -f frontend
```

