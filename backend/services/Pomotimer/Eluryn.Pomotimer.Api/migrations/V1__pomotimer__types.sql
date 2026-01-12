create schema if not exists pomotimer;
set search_path to pomotimer;

do $$
begin
  if not exists (select 1 from pg_type t join pg_namespace n on n.oid = t.typnamespace
                 where t.typname = 'pomodoro_interval_type' and n.nspname = 'pomotimer') then
    create type pomodoro_interval_type as enum ('Focus', 'ShortBreak', 'LongBreak');
  end if;

  if not exists (select 1 from pg_type t join pg_namespace n on n.oid = t.typnamespace
                 where t.typname = 'pomodoro_session_status' and n.nspname = 'pomotimer') then
    create type pomodoro_session_status as enum ('Running', 'Paused', 'Completed', 'Cancelled');
  end if;
end
$$;
