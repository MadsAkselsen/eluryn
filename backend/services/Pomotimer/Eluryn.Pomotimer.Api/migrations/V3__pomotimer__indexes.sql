set search_path to pomotimer;

create index if not exists ix_pomodoro_sessions_user_created
  on pomodoro_sessions (user_id, created_at_utc desc);

create index if not exists ix_focus_log_entries_user_started
  on focus_log_entries (user_id, started_at_utc desc);

create index if not exists ix_focus_log_entries_session_started
  on focus_log_entries (session_id, started_at_utc asc);
