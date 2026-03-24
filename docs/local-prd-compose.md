````md
# Local Production-like Environment

This setup simulates a production environment locally using Docker Compose.

It includes:
- Traefik (reverse proxy + TLS)
- Nginx (serves frontend build)
- .NET backend services
- PostgreSQL
- Flyway migrations

---

## 🚀 Run

From project root:

```bash
./compose/prd/scripts/prod-local-up.sh
````

Stop:

```bash
./compose/prd/scripts/prod-local-down.sh
```

Reset (including DB):

```bash
./compose/prd/scripts/prod-local-reset.sh
```

---

## 🌐 URLs

* Frontend: [https://app.localhost](https://app.localhost)
* APIs: [https://api.localhost](https://api.localhost)

---

## 🔐 HTTPS (mkcert)

Generate local certificates:

```bash
mkcert -cert-file ./certs/local-cert.pem \
       -key-file ./certs/local-key.pem \
       localhost api.localhost app.localhost 127.0.0.1 ::1
```

⚠️ The certificate must include:

* `localhost`
* `api.localhost`
* `app.localhost`

---

## 🧱 Architecture

Browser → Traefik → (nginx + APIs) → PostgreSQL

---

## 🧠 Key differences from local dev

| Local Dev       | Local Prod      |
| --------------- | --------------- |
| Vite dev server | Nginx build     |
| No TLS          | Traefik + HTTPS |
| Hot reload      | Static files    |

---

## ⚠️ Common issues

### Port already in use

```bash
docker ps
docker stop <container>
```

---

### Frontend build fails (cert error)

Cause: Vite tries to load local certs during build
Fix: only load certs when `command === 'serve'` in `vite.config.ts`

---

### HTTPS not secure

Cause: missing hostname in certificate
Fix: regenerate cert including `app.localhost`

---

### Changes not applied

```bash
docker compose up --build
```

