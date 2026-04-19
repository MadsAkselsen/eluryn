# Eluryn prd CI/CD

## Image Names

Images are published to the registry configured in the workflow:

- `<registry>/<owner>/eluryn-frontend:sha-<commit-sha>`
- `<registry>/<owner>/eluryn-users-api:sha-<commit-sha>`
- `<registry>/<owner>/eluryn-pomotimer-api:sha-<commit-sha>`

The workflow also publishes a mutable `:prd` tag for quick inspection, but the droplet deploys the immutable `sha-<commit-sha>` tag.

## Deployment Settings

These non-secret deployment settings are defined in the workflow:

- `REGISTRY`: `ghcr.io`
- `DEPLOY_APP_DIR`: `/opt/eluryn`
- `DEPLOY_COMPOSE_SOURCE`: `compose/prd/compose.yml`
- `DEPLOY_COMPOSE_FILE`: `compose.yml`
- `DEPLOY_SCRIPT_SOURCE`: `scripts/deploy/deploy`
- `DEPLOY_SCRIPT_FILE`: `deploy`
- `DEPLOY_ENV_FILE`: `.env`
- `COMPOSE_PROJECT_NAME`: `eluryn`

## GitHub Variables

Set these in the `prd` GitHub Environment:

- `APP_HOST`: `eluryn.com`
- `API_HOST`: `api.eluryn.com`
- `VITE_API_BASE_URL`: `https://api.eluryn.com`
- `TRAEFIK_ACME_EMAIL`: email for Let's Encrypt notices
- `DB_PORT`: DigitalOcean Postgres port, usually `25060`
- `DB_NAME`: prd database name
- `USERS_DB_USER`: `users_svc`
- `POMOTIMER_DB_USER`: `pomotimer_svc`

## GitHub Secrets

Set these in the `prd` GitHub Environment:

- `DROPLET_HOST`
- `DROPLET_USER`
- `DROPLET_SSH_PRIVATE_KEY`
- `GHCR_READ_TOKEN` or `REGISTRY_READ_TOKEN`: a fine-grained token or classic PAT with package read access
- `REGISTRY_PASSWORD`: only needed if image pushes should not use the default `GITHUB_TOKEN`
- `DB_HOST`: private DigitalOcean Postgres host reachable from the droplet
- `USERS_DB_PASSWORD`
- `POMOTIMER_DB_PASSWORD`

## Droplet Requirements

Install Docker Engine and Docker Compose v2 on the droplet. The workflow creates and owns the configured app directory, uploads the configured compose file and deploy script, uploads migrations, writes the configured env file, logs in to the container registry, and runs the deploy script with the target environment argument.

The deploy script pulls images, runs Flyway migrations against the managed Postgres cluster, starts the stack, and prunes images older than seven days.

## Migration Strategy

Migrations run on the droplet during deployment. This is the pragmatic first version because the managed Postgres cluster is reachable from the droplet over private networking, while GitHub-hosted runners typically are not.

The tradeoff is that the droplet temporarily has DB migration credentials. That is acceptable for this single-droplet setup because the app already needs DB credentials there. Later, move migration execution to a private GitHub runner or a short-lived deployment job inside the VPC if you want cleaner separation.

## First Rollout Order

1. Add the GitHub variables and secrets.
2. Install Docker and Docker Compose v2 on the droplet.
3. Ensure Cloudflare DNS points `eluryn.com` and `api.eluryn.com` to the droplet and ports 80/443 are open.
4. Push to `prd` and let the CI workflow pass.
5. Confirm Traefik issued certificates and all services are healthy.
6. Let `prd` pushes deploy automatically.

Short-term acceptable compromises: one droplet does both runtime and migration execution, deploys are rolling only at the container level, and the droplet keeps a GHCR read token. Good follow-ups are backup/restore drills, smoke tests after deploy, Dependabot, image vulnerability scanning, and a staging environment.
