set search_path to pomotimer;

create table if not exists pomodoro_settings (
  user_id uuid primary key,
  focus_seconds int not null,
  short_break_seconds int not null,
  long_break_seconds int not null,
  long_break_interval int not null,
  created_at_utc timestamptz not null default now(),
  updated_at_utc timestamptz not null default now()
);

create table if not exists user_preferences (
  user_id uuid primary key,
  theme text not null,
  sound_enabled boolean not null default true,
  auto_start_next_interval boolean not null default true,
  created_at_utc timestamptz not null default now(),
  updated_at_utc timestamptz not null default now()
);

create table if not exists pomodoro_sessions (
  id uuid primary key,
  user_id uuid not null,
  status pomodoro_session_status not null,
  started_at_utc timestamptz not null,
  ended_at_utc timestamptz,
  created_at_utc timestamptz not null default now(),
  updated_at_utc timestamptz not null default now()
);

create table if not exists focus_log_entries (
  id uuid primary key,
  user_id uuid not null,
  session_id uuid not null references pomodoro_sessions(id) on delete cascade,
  interval_type pomodoro_interval_type not null,
  started_at_utc timestamptz not null,
  ended_at_utc timestamptz,
  duration_seconds int not null,
  created_at_utc timestamptz not null default now()
);

create table if not exists user_focus_all_time (
  user_id uuid primary key,
  focus_seconds int not null,
  created_at_utc timestamptz not null default now(),
  updated_at_utc timestamptz not null default now()
);
