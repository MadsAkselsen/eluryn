## Step 1 - Install mkcert (per dev, one time)

Depends on OS:

- macOS:
    ```bash
    brew install mkcert nss
    ```

- Ubuntu: 
    ```bash
    sudo apt update
    sudo apt install libnss3-tools mkcert
    ```

- Windows:
    ```bash
    choco install mkcert
    ```

## Step 2 - Install a local CA into your OS/browser trust store

```bash
mkcert -install
```

## Step 3 – Trust the mkcert CA in Windows (WSL only)

Because your browser runs on Windows, Windows must trust the same mkcert CA.

### 3.1 Get the mkcert CA path in WSL

```bash
mkcert -CAROOT
```

### 3.2 Copy the CA to Windows

Replace <WINUSER> with your Windows username:

```bash
cp "$(mkcert -CAROOT)/rootCA.pem" "/mnt/c/Users/<WINUSER>/Downloads/"
```

### 3.3 Import into Windows trust store

1. Press Win + R

2. Run:
    ```bash
    certmgr.msc
    ```

3. Go to:
    ```bash
    Trusted Root Certification Authorities
    └── Certificates
    ```

4. Right-click → All Tasks → Import

5. Select rootCA.pem
   - f it doesn’t show up, change file type to All Files (.)

6. Finish the wizard

7. Opional but safe

    Repeat the import using Local Machine store via mmc.

## Step 4  Generate local certs for the domains you use

From the root of the repo run:

```bash
mkcert \
  -key-file certs/local-key.pem \
  -cert-file certs/local-cert.pem \
  localhost \
  127.0.0.1 \
  ::1 \
  api.localhost
```

Now you have:

- certs/local-cert.pem
- certs/local-key.pem

These are used by vite.config.ts

### Step 5 – Verify the certificate SANs

```bash
openssl x509 -in certs/local-cert.pem -noout -text | grep -A1 "Subject Alternative Name"
```

### Step 6 – Common WSL / Windows fixes

If you still see a browser warning after doing everything above:

Clear Windows SSL cache

- Content tab → Clear SSL state

- Restart browser

Clear HSTS for localhost

```bash
edge://net-internals/#hsts
```

Delete policies for:

- localhost

- api.localhost

- Restart browser

Quick workaround

```bash
https://127.0.0.1:5173
```

If this works but https://localhost:5173 does not, the issue is almost always missing ::1 in the cert.

#### Notes about Vite

Recent Vite versions do not support a --https CLI flag.
HTTPS must be configured in vite.config.ts using the generated cert files.