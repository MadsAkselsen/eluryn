## Changing Pomodoro settings during active Pomodoro session

This Sequence diagram is illustrating what happens if user resumes the pomodoro timer or refreshes the web page during an active timer.

## Sequence Diagram

```mermaid
sequenceDiagram
    autonumber
    actor U as User
    participant UI as Web UI
    participant API as Pomodoro API
    participant SVC as PomodoroService
    participant SessRepo as PomodoroSessionRepo
    participant LogRepo as FocusLogRepo

    Note over UI,SVC: Pomodoro interval was started earlier

    U->>UI: Open app / refresh
    UI->>API: GET /pomodoro/current
    API->>SVC: GetCurrent(userId, nowUtc)
    SVC->>SessRepo: GetActiveSession(userId)
    SessRepo-->>SVC: session

    SVC->>SVC: Reconcile(nowUtc) using session.startTime + configSnapshot

    alt Interval already ended while app was closed
        SVC->>SVC: Mark interval completed
        SVC->>LogRepo: Append FocusLogEntry(actualDuration)
        LogRepo-->>SVC: OK
        SVC->>SessRepo: Update session (advance phase)
        SessRepo-->>SVC: OK
    else Interval still active
        Note over SVC: No state change
    end

    SVC-->>API: PomodoroStateDto
    API-->>UI: 200 OK
    UI->>UI: Calculate remaining time and render timeline

```