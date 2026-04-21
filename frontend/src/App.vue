<script setup lang="ts">
import { computed, ref } from "vue";

import {
  getPomodoroSettings,
  getPomotimerHealth,
  putPomodoroSettings,
  type PomodoroSettingsRequest,
} from "@/api/pomotimer";
import backgroundPlaceholderUrl from "../../media/background-placeholder.png";

const userId = ref("11111111-1111-1111-1111-111111111111");
const form = ref<PomodoroSettingsRequest>({
  focusSeconds: 1500,
  shortBreakSeconds: 300,
  longBreakSeconds: 900,
  longBreakInterval: 4,
});

const loading = ref(false);
const error = ref<string | null>(null);
const result = ref<unknown>(null);
const lastAction = ref<string | null>(null);

const apiBaseUrl = computed(() => import.meta.env.VITE_API_BASE_URL || "not configured");
const backgroundStyle = computed(() => ({
  backgroundImage: `url(${backgroundPlaceholderUrl})`,
}));

async function runRequest(action: string, request: () => Promise<unknown>) {
  loading.value = true;
  error.value = null;
  lastAction.value = action;

  try {
    result.value = await request();
  } catch (e) {
    error.value = (e as Error).message;
  } finally {
    loading.value = false;
  }
}

function checkHealth() {
  return runRequest("GET /pomotimer/health", getPomotimerHealth);
}

function fetchSettings() {
  return runRequest("GET pomodoro settings", () => getPomodoroSettings(userId.value));
}

function saveSettings() {
  return runRequest("PUT pomodoro settings", () =>
    putPomodoroSettings(userId.value, form.value),
  );
}
</script>

<template>
  <main class="app-background" :style="backgroundStyle">
    <div class="endpoint-tester">

    </div>
  </main>
</template>

<style scoped>
.app-background {
  width: 100vw;
  min-height: 100dvh;
  margin-inline: calc(50% - 50vw);
  display: grid;
  place-items: center;
  padding: clamp(1rem, 4vw, 3rem);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}

.endpoint-tester {
  width: min(960px, 100%);
  display: grid;
  gap: 1.5rem;
}

.intro {
  display: grid;
  gap: 0.5rem;
}

.eyebrow {
  color: #0f766e;
  font-weight: 700;
  text-transform: uppercase;
}

h1 {
  color: var(--color-heading);
  font-size: 2.4rem;
  font-weight: 800;
  line-height: 1.1;
}

.tester-panel,
.response-panel {
  border: 1px solid var(--color-border);
  border-radius: 8px;
  padding: 1rem;
  background: var(--color-background-soft);
}

label {
  display: grid;
  gap: 0.35rem;
  font-weight: 700;
}

input {
  width: 100%;
  border: 1px solid var(--color-border-hover);
  border-radius: 8px;
  padding: 0.75rem;
  color: var(--color-text);
  background: var(--color-background);
}

.settings-grid {
  display: grid;
  gap: 1rem;
  margin-top: 1rem;
}

.actions {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
  margin-top: 1rem;
}

button {
  border: 0;
  border-radius: 8px;
  padding: 0.75rem 1rem;
  color: white;
  background: #1d4ed8;
  cursor: pointer;
  font-weight: 700;
}

button:disabled {
  cursor: wait;
  opacity: 0.65;
}

.response-heading {
  display: flex;
  flex-wrap: wrap;
  align-items: baseline;
  justify-content: space-between;
  gap: 0.5rem;
  margin-bottom: 0.75rem;
}

h2 {
  font-size: 1.2rem;
  font-weight: 800;
}

.response-heading span {
  font-family: monospace;
}

.error {
  color: #b91c1c;
  font-weight: 700;
}

pre {
  overflow: auto;
  border-radius: 8px;
  padding: 1rem;
  color: #e5e7eb;
  background: #111827;
}

@media (min-width: 720px) {
  .settings-grid {
    grid-template-columns: repeat(2, minmax(0, 1fr));
  }
}
</style>
