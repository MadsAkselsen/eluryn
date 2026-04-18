# Local development

A local development environment is set up using Docker Compose.

## Run, stop, restart

<!-- #### Start everything (with frontend watch mode) -->

<!-- ```bash
docker compose -f compose/local/compose.local.yml watch
``` -->

#### Stop all containers (clean shutdown)

```bash
docker compose -p eluryn -f compose/local/compose.local.yml down
```

#### Restart a backend service:

```bash
docker compose -p eluryn -f compose/local/compose.local.yml restart users-api
docker compose -p eluryn -f compose/local/compose.local.yml restart pomotimer-api
```

#### Full reset (wipe DBs and rebuild everything - but not deleting DBs)

```bash
docker compose -p eluryn -f ./compose/local/compose.local.yml down --remove-orphans && docker network rm eluryn-edge eluryn-internal 2>/dev/null || true && docker compose -p eluryn -f ./compose/local/compose.local.yml up --build
```

## Debugging and logs

#### Show container status:

```bash
docker compose -p eluryn -f compose/local/compose.local.yml ps
```

#### View logs for a specific service:

```bash
docker compose -p eluryn -f compose/local/compose.local.yml logs -f --tail=200 users-api
```

## Tests

<!-- #### Run E2E playwright tests same way as in pipeline

```bash
docker compose -f compose/local/compose.local.yml -f compose/local/compose.e2e.yml up --build --abort-on-container-exit --exit-code-from e2e
``` -->

