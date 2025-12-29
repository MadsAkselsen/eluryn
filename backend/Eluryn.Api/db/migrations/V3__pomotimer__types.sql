do $$
begin
  if not exists (select 1 from pg_type t join pg_namespace n on n.oid = t.typnamespace
                 where t.typname = 'pomodoro_session_status' and n.nspname = 'pomotimer') then
    create type pomotimer.pomodoro_session_status as enum (
      'running',
      'paused',
      'completed',
      'cancelled'
    );
  end if;

  if not exists (select 1 from pg_type t join pg_namespace n on n.oid = t.typnamespace
                 where t.typname = 'pomodoro_interval_type' and n.nspname = 'pomotimer') then
    create type pomotimer.pomodoro_interval_type as enum (
      'focus',
      'short_break',
      'long_break'
    );
  end if;
end $$;
