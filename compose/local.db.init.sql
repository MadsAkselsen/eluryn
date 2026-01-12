-- Roles (service users)
DO $$
BEGIN
  IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'users_svc') THEN
    CREATE ROLE users_svc LOGIN PASSWORD 'users_pw';
  END IF;

  IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'pomotimer_svc') THEN
    CREATE ROLE pomotimer_svc LOGIN PASSWORD 'pomo_pw';
  END IF;
END
$$;

-- Schemas
CREATE SCHEMA IF NOT EXISTS users;
CREATE SCHEMA IF NOT EXISTS pomotimer;

-- Give each service user access only to its schema
GRANT USAGE, CREATE ON SCHEMA users TO users_svc;
GRANT USAGE, CREATE ON SCHEMA pomotimer TO pomotimer_svc;

-- Ensure future tables/sequences created by migrations are usable by the service user
ALTER DEFAULT PRIVILEGES IN SCHEMA users
  GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO users_svc;
ALTER DEFAULT PRIVILEGES IN SCHEMA users
  GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO users_svc;

ALTER DEFAULT PRIVILEGES IN SCHEMA pomotimer
  GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO pomotimer_svc;
ALTER DEFAULT PRIVILEGES IN SCHEMA pomotimer
  GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO pomotimer_svc;

-- Make each role default into its schema (so unqualified SQL targets that schema)
ALTER ROLE users_svc SET search_path = users;
ALTER ROLE pomotimer_svc SET search_path = pomotimer;
