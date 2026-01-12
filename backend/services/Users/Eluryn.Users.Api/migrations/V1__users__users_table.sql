create schema if not exists users;
set search_path to users;

create table if not exists users (
  id uuid primary key,
  username text not null,
  created_at_utc timestamptz not null default now(),
  updated_at_utc timestamptz not null default now()
);

create unique index if not exists ux_users_username
  on users (username);
