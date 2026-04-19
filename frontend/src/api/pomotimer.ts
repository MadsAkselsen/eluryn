import { apiGet, apiPut } from "./http";

export type PomotimerHealth = {
  status: string;
};

export type PomodoroSettingsRequest = {
  focusSeconds: number;
  shortBreakSeconds: number;
  longBreakSeconds: number;
  longBreakInterval: number;
};

export type PomodoroSettingsResponse = PomodoroSettingsRequest & {
  userId: string;
  createdAtUtc: string;
  updatedAtUtc: string;
};

const pomodoroSettingsPath = (userId: string) =>
  `/pomotimer/api/pomodoro-settings/${userId}`;

export function getPomotimerHealth() {
  return apiGet<PomotimerHealth>("/pomotimer/health");
}

export function getPomodoroSettings(userId: string) {
  return apiGet<PomodoroSettingsResponse>(pomodoroSettingsPath(userId));
}

export function putPomodoroSettings(userId: string, request: PomodoroSettingsRequest) {
  return apiPut<PomodoroSettingsResponse>(pomodoroSettingsPath(userId), request);
}
