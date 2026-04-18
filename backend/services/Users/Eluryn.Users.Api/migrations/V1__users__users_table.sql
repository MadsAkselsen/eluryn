CREATE TABLE IF NOT EXISTS users.users (
  id uuid PRIMARY KEY,
  username text NOT NULL,
  created_at_utc timestamptz NOT NULL DEFAULT now(),
  updated_at_utc timestamptz NOT NULL DEFAULT now()
);

CREATE UNIQUE INDEX IF NOT EXISTS ux_users_username
  ON users.users (username);
