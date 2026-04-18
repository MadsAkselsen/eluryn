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

GRANT CONNECT ON DATABASE eluryn TO users_svc;
GRANT CONNECT ON DATABASE eluryn TO pomotimer_svc;

CREATE SCHEMA IF NOT EXISTS users;
CREATE SCHEMA IF NOT EXISTS pomotimer;

GRANT USAGE, CREATE ON SCHEMA users TO users_svc;
GRANT USAGE, CREATE ON SCHEMA pomotimer TO pomotimer_svc;

ALTER ROLE users_svc SET search_path = users;
ALTER ROLE pomotimer_svc SET search_path = pomotimer;