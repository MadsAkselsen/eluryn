-- Sessions: common query patterns (user + recency)
create index if not exists ix_pomodoro_sessions_user_created
  on pomodoro_sessions (user_id, created_at_utc desc);

-- Focus logs: common query patterns
create index if not exists ix_focus_log_entries_user_started
  on focus_log_entries (user_id, started_at_utc desc);

create index if not exists ix_focus_log_entries_session_started
  on focus_log_entries (session_id, started_at_utc asc);

-- Aggregates: fetch recent periods fast (PK already helps, these are just nice)
create index if not exists ix_user_focus_daily_user_date_desc
  on user_focus_daily (user_id, date_utc desc);

create index if not exists ix_user_focus_weekly_user_week_desc
  on user_focus_weekly (user_id, week_start_utc desc);

create index if not exists ix_user_focus_monthly_user_month_desc
  on user_focus_monthly (user_id, month_start_utc desc);
