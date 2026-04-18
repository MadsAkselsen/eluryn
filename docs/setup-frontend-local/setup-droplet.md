# Eluryn – Infrastructure Setup (Droplet)

This guide sets up Docker, initializes the database, and runs migrations.

---

## 1. Install Docker on Ubuntu (Droplet)

```bash
apt update
apt install -y ca-certificates curl

install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
chmod a+r /etc/apt/keyrings/docker.asc

cat > /etc/apt/sources.list.d/docker.sources <<EOF
Types: deb
URIs: https://download.docker.com/linux/ubuntu
Suites: $(. /etc/os-release && echo "${UBUNTU_CODENAME:-$VERSION_CODENAME}")
Components: stable
Architectures: $(dpkg --print-architecture)
Signed-By: /etc/apt/keyrings/docker.asc
EOF

apt update
apt install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
```

---

## 2. Generate Secure Passwords

Generate strong passwords for each database user:

```bash
openssl rand -hex 32
```

Store these securely (password manager or environment variables).

---

## 3. Database Initialization Script

Replace placeholders before running.

```sql
DO $$
BEGIN
  IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'users_svc') THEN
    CREATE ROLE users_svc LOGIN PASSWORD '${USERS_DB_PASSWORD}';
  END IF;

  IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'pomotimer_svc') THEN
    CREATE ROLE pomotimer_svc LOGIN PASSWORD '${POMOTIMER_DB_PASSWORD}';
  END IF;
END
$$;

ALTER ROLE users_svc WITH PASSWORD '${USERS_DB_PASSWORD}';
ALTER ROLE pomotimer_svc WITH PASSWORD '${POMOTIMER_DB_PASSWORD}';

CREATE SCHEMA IF NOT EXISTS users AUTHORIZATION users_svc;
CREATE SCHEMA IF NOT EXISTS pomotimer AUTHORIZATION pomotimer_svc;

GRANT USAGE, CREATE ON SCHEMA users TO users_svc;
GRANT USAGE, CREATE ON SCHEMA pomotimer TO pomotimer_svc;

ALTER ROLE users_svc IN DATABASE defaultdb SET search_path = users;
ALTER ROLE pomotimer_svc IN DATABASE defaultdb SET search_path = pomotimer;
```

---

## 4. Connect to Droplet

```bash
ssh YOUR_USER@YOUR_DROPLET_IP
```

> Tip: Use a non-root user with sudo access.

---

## 5. Connect to Database

```bash
psql "host=YOUR_DB_HOST port=25060 user=YOUR_DB_ADMIN_USER dbname=defaultdb sslmode=require"
```

---

## 6. Clone Repository

```bash
git clone https://github.com/YOUR_GITHUB_USERNAME/YOUR_REPO.git
cd YOUR_REPO
```

---

## 7. Run Database Migrations (Flyway)

Example for Pomotimer service:

```bash
docker run --rm \
  -v "$(pwd)/backend/services/Pomotimer/Eluryn.Pomotimer.Api/migrations:/flyway/sql:ro" \
  flyway/flyway:12.0.0 \
  -url=jdbc:postgresql://YOUR_DB_HOST:25060/defaultdb \
  -user=pomotimer_svc \
  -password="$POMOTIMER_DB_PASSWORD" \
  -connectRetries=10 \
  -schemas=pomotimer \
  -locations=filesystem:/flyway/sql \
  -baselineOnMigrate=true \
  migrate
```

Repeat for other services as needed.

---

## 🔐 Security Notes

- Never commit real passwords, tokens, or secrets
- Use environment variables or a secret manager
- Do not expose:
  - real IP addresses
  - database hostnames
  - admin usernames
- Prefer SSH keys over password login
- Disable root SSH login in production

---

## 📌 Example `.env` (DO NOT COMMIT)

```env
USERS_DB_PASSWORD=your_secure_password
POMOTIMER_DB_PASSWORD=your_secure_password
DB_HOST=your-db-host
DB_ADMIN_USER=your-admin-user
```

Add `.env` to `.gitignore`.

---

## 🧠 Tip

This file is a template / onboarding guide — not a place to store secrets.