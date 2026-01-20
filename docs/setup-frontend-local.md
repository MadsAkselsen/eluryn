## Step 1 - Install mkcert (per dev, one time)

Depends on OS:

- macOS: brew install mkcert nss

- Ubuntu: sudo apt install libnss3-tools + download mkcert binary (or package)

- Windows: choco install mkcert

## Step 2 - Install a local CA into your OS/browser trust store

```bash
mkcert -install
```

## Step 3 - Generate certs for the domains you use

From the root of the repo run:

```bash
mkcert -key-file certs/local-key.pem -cert-file certs/local-cert.pem localhost 127.0.0.1 api.localhost
```

Now you have:

- certs/local-cert.pem
- certs/local-key.pem

These are used by vite.config.ts

