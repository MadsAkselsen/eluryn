```mermaid
erDiagram
    WEB_USERS ||--|| POMODORO_SETTINGS : has
    WEB_USERS ||--|| USER_PREFERENCES : has
    WEB_USERS ||--o{ POMODORO_SESSIONS : owns
    WEB_USERS ||--o{ FOCUS_LOG_ENTRIES : has
    POMODORO_SESSIONS ||--o{ FOCUS_LOG_ENTRIES : contains

    WEB_USERS {
        uuid id PK
        string username
    }

    POMODORO_SETTINGS {
        uuid user_id PK, FK
        int focus_seconds
        int short_break_seconds
        int long_break_seconds
        int long_break_interval
        datetime updated_at_utc
    }

    USER_PREFERENCES {
        uuid user_id PK, FK
        string theme
        boolean sound_enabled
        boolean auto_start_next_interval
        string locale
        datetime updated_at_utc
    }

    POMODORO_SESSIONS {
        uuid id PK
        uuid user_id FK
        string status
        string current_interval_type
        int phase
        datetime start_time_utc
        datetime paused_at_utc
        int accumulated_pause_seconds

        int snapshot_focus_seconds
        int snapshot_short_break_seconds
        int snapshot_long_break_seconds
        int snapshot_long_break_interval

        datetime created_at_utc
        datetime updated_at_utc
    }

    FOCUS_LOG_ENTRIES {
        uuid id PK
        uuid user_id FK
        uuid session_id FK
        string interval_type
        datetime started_at_utc
        datetime ended_at_utc
        int duration_seconds
        int pause_seconds
        boolean ended_early
    }

```