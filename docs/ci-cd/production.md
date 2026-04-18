# Eluryn Production CI/CD

## Image Names

Production images are published to GitHub Container Registry:

- `ghcr.io/<owner>/eluryn-frontend:sha-<commit-sha>`
- `ghcr.io/<owner>/eluryn-users-api:sha-<commit-sha>`
- `ghcr.io/<owner>/eluryn-pomotimer-api:sha-<commit-sha>`

The workflow also publishes a mutable `:main` tag for quick inspection, but the droplet deploys the immutable `sha-<commit-sha>` tag.

## GitHub Variables

Set these in the repository or production environment:

- `APP_HOST`: `eluryn.com`
- `API_HOST`: `api.eluryn.com`
- `VITE_API_BASE_URL`: `https://api.eluryn.com`
- `TRAEFIK_ACME_EMAIL`: email for Let's Encrypt notices
- `DB_PORT`: DigitalOcean Postgres port, usually `25060`
- `DB_NAME`: production database name
- `USERS_DB_USER`: `users_svc`
- `POMOTIMER_DB_USER`: `pomotimer_svc`

## GitHub Secrets

Set these in the repository or production environment:

- `DROPLET_HOST`
- `DROPLET_USER`
- `DROPLET_SSH_KEY`
- `GHCR_READ_TOKEN`: a fine-grained token or classic PAT with `read:packages`
- `DB_HOST`: private DigitalOcean Postgres host reachable from the droplet
- `USERS_DB_PASSWORD`
- `POMOTIMER_DB_PASSWORD`

## Droplet Requirements

Install Docker Engine and Docker Compose v2 on the droplet. The workflow creates and owns `/opt/eluryn`, uploads `compose.prod.yml`, uploads migrations, writes `/opt/eluryn/.env`, logs in to GHCR, and runs the deploy script.

The deploy script pulls images, runs Flyway migrations against the managed Postgres cluster, starts the stack, and prunes images older than seven days.

## Migration Strategy

Migrations run on the droplet during deployment. This is the pragmatic first version because the managed Postgres cluster is reachable from the droplet over private networking, while GitHub-hosted runners typically are not.

The tradeoff is that the droplet temporarily has DB migration credentials. That is acceptable for this single-droplet setup because the app already needs DB credentials there. Later, move migration execution to a private GitHub runner or a short-lived deployment job inside the VPC if you want cleaner separation.

## First Rollout Order

1. Add the GitHub variables and secrets.
2. Install Docker and Docker Compose v2 on the droplet.
3. Ensure Cloudflare DNS points `eluryn.com` and `api.eluryn.com` to the droplet and ports 80/443 are open.
4. Run the production workflow manually with `workflow_dispatch`.
5. Confirm Traefik issued certificates and all services are healthy.
6. Let `main` pushes deploy automatically.

Short-term acceptable compromises: one droplet does both runtime and migration execution, deploys are rolling only at the container level, and the droplet keeps a GHCR read token. Good follow-ups are backup/restore drills, smoke tests after deploy, Dependabot, image vulnerability scanning, and a staging environment.
