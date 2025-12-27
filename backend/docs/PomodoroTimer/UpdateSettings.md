## Changing Pomodoro settings during active Pomodoro session

This Sequence diagram is illustrating what happens if user changes Pomodoro Settings while a pomodoro sessions is running.

User updates settings → current interval is NOT affected → new settings apply next interval.

## Sequence Diagram

```mermaid
sequenceDiagram
    autonumber
    actor U as User
    participant UI as Web UI
    participant API as Pomodoro API
    participant SVC as PomodoroService
    participant PrefRepo as PomodoroSettingsRepo

    Note over UI,SVC: A focus interval is already running

    U->>UI: Change focus length (55 → 45)
    UI->>API: PUT /settings/pomodoro { focus=45, break=5, longBreak=60 }
    API->>SVC: UpdatePomodoroSettings(userId, newSettings)
    SVC->>PrefRepo: Save(newSettings)
    PrefRepo-->>SVC: OK
    SVC-->>API: OK
    API-->>UI: 204 No Content

    Note over UI: Current interval continues unchanged\nNew settings apply from next interval
```