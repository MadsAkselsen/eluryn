<script setup lang="ts">
import { computed, ref } from 'vue'

import { usePomodoroStore } from '@/stores/pomodoro'
// import { useRoomStore } from '@/stores/room'
import { useUiStore } from '@/stores/ui'

const pomodoro = usePomodoroStore()
// const room = useRoomStore()
const ui = useUiStore()
const isPomodoroMenuOpen = ref(false)

const markerStyle = computed(() => ({
  left: `${Math.min(Math.max(pomodoro.progress, 0), 1) * 100}%`,
}))
</script>

<template>
  <main
    class="room-view"
    :style="ui.backgroundImageStyle"
  >
    <section class="timer-bar" aria-label="Pomodoro timer">
      <div class="timer-heading">
        <button
          type="button"
          class="timer-time"
          :aria-expanded="isPomodoroMenuOpen"
          @click="isPomodoroMenuOpen = !isPomodoroMenuOpen"
        >
          {{ pomodoro.displayTime }}
        </button>
      </div>

      <div class="timeline" aria-hidden="true">
        <span class="line"></span>
        <span class="progress-line" :style="{ width: markerStyle.left }"></span>
        <span class="dot active" :style="markerStyle"></span>
        <span class="dot marker-1"></span>
        <span class="dot marker-2"></span>
        <span class="dot marker-3"></span>
      </div>

      <div class="labels" aria-hidden="true">
        <span>25m</span>
        <span>5m</span>
        <span>25m</span>
        <span>5m</span>
        <span>25m</span>
      </div>

      <div v-if="isPomodoroMenuOpen" class="pomodoro-menu">
        <button type="button" @click="ui.goToLanding">Eluryn</button>
        <button type="button" @click="ui.openProfile">Profile</button>
        <button type="button" @click="ui.openSettings">Settings</button>
        <button
          type="button"
          class="primary"
          @click="pomodoro.isRunning ? pomodoro.pause() : pomodoro.start()"
        >
          {{ pomodoro.isRunning ? 'Pause' : 'Start' }}
        </button>
        <button type="button" @click="pomodoro.skip">Skip</button>
        <button type="button" @click="pomodoro.reset">Reset</button>
      </div>
    </section>

    <!-- <aside class="chat-panel" aria-label="Room chat">
      <p v-for="message in room.chatMessages" :key="message.id">
        <strong>{{ message.author }}</strong>
        {{ message.body }}
      </p>
    </aside> -->
  </main>
</template>

<style scoped>
.room-view {
  min-height: 100dvh;
  display: grid;
  align-items: start;
  justify-items: center;
  position: relative;
  background-color: #211711;
  background-position: center bottom;
  background-repeat: no-repeat;
  background-size: cover;
}

.timer-bar {
  position: absolute;
  right: 0;
  top: 0;
  left: 0;
  z-index: 2;
  width: min(100%, 430px);
  display: grid;
  justify-items: center;
  gap: 0.35rem;
  margin: 0 auto;
  padding: 2.1rem clamp(1rem, 5vw, 2rem) 1.15rem;
  color: #f8ecd1;
  text-shadow:
    0 2px 8px rgba(0, 0, 0, 0.9),
    0 0 18px rgba(0, 0, 0, 0.82);
}

.timer-bar::before,
.timer-bar::after {
  position: absolute;
  z-index: -1;
  content: '';
  pointer-events: none;
}

.timer-bar::before {
  inset: 0 -2.5rem -0.75rem;
  background:
    linear-gradient(
      rgba(10, 7, 5, 0.36),
      rgba(10, 7, 5, 0.3) 34%,
      rgba(10, 7, 5, 0.14) 68%,
      rgba(10, 7, 5, 0)
    ),
    radial-gradient(
      ellipse at center 42%,
      rgba(8, 5, 4, 0.58),
      rgba(8, 5, 4, 0.22) 56%,
      rgba(8, 5, 4, 0) 82%
    );
  filter: blur(14px);
}

.timer-bar::after {
  right: -1.5rem;
  bottom: -0.35rem;
  left: -1.5rem;
  height: 3.4rem;
  background: radial-gradient(
    ellipse at center,
    rgba(6, 4, 3, 0.68),
    rgba(6, 4, 3, 0.34) 52%,
    rgba(6, 4, 3, 0) 82%
  );
  filter: blur(10px);
}

.timer-heading {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.7rem;
}

.timer-heading span {
  width: 5px;
  height: 5px;
  border-radius: 50%;
  background: rgba(255, 226, 160, 0.75);
}

.timer-heading p {
  color: rgba(255, 238, 200, 0.8);
  font-size: 0.95rem;
  font-weight: 600;
}

.timer-time {
  border: 0;
  color: #fff2d4;
  background: transparent;
  cursor: pointer;
  font-size: clamp(1.6rem, 5vw, 2rem);
  font-weight: 500;
  line-height: 1;
  letter-spacing: 0;
}

.timer-time[aria-expanded='true'] {
  color: #ffe2a0;
}

.timeline {
  position: relative;
  width: min(360px, 74vw);
  height: 16px;
}

.line {
  position: absolute;
  inset-inline: 0;
  top: 50%;
  height: 2px;
  background: rgba(227, 210, 151, 0.26);
  transform: translateY(-50%);
}

.progress-line {
  position: absolute;
  left: 0;
  top: 50%;
  z-index: 1;
  height: 2px;
  background: rgba(255, 215, 139, 0.9);
  transform: translateY(-50%);
}

.dot {
  position: absolute;
  top: 50%;
  z-index: 2;
  width: 5px;
  height: 5px;
  border-radius: 50%;
  background: rgba(210, 193, 141, 0.52);
  transform: translate(-50%, -50%);
}

.dot.active {
  width: 7px;
  height: 7px;
  background: #ffe2a0;
  box-shadow: 0 0 12px rgba(255, 226, 160, 0.42);
}

.marker-1 {
  left: 25%;
}

.marker-2 {
  left: 50%;
}

.marker-3 {
  left: 75%;
}

.labels {
  width: min(360px, 74vw);
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  color: rgba(255, 238, 200, 0.7);
  font-size: clamp(0.72rem, 2.4vw, 0.85rem);
  text-align: center;
}

.pomodoro-menu {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 0.45rem;
  margin-top: 0.25rem;
  padding: 0.45rem;
  border: 1px solid rgba(239, 204, 138, 0.28);
  border-radius: 8px;
  background: rgba(18, 12, 9, 0.78);
  box-shadow: 0 12px 28px rgba(0, 0, 0, 0.42);
  backdrop-filter: blur(6px);
}

.pomodoro-menu button {
  min-height: 30px;
  border: 1px solid rgba(239, 204, 138, 0.34);
  border-radius: 6px;
  color: #f8ecd1;
  background: rgba(73, 47, 25, 0.72);
  cursor: pointer;
  font-weight: 700;
}

.pomodoro-menu .primary {
  color: #2a1202;
  background: linear-gradient(#efb34d, #a8510b);
}

button:hover {
  filter: brightness(1.08);
}

.chat-panel {
  align-self: end;
  justify-self: end;
  width: min(340px, calc(100vw - 1rem));
  margin: 0 0.5rem 0.5rem;
  padding: 0.75rem;
  border: 1px solid rgba(239, 204, 138, 0.24);
  border-radius: 8px;
  color: #f8ecd1;
  background: rgba(21, 15, 11, 0.66);
}

.chat-panel p {
  display: grid;
  gap: 0.15rem;
  color: rgba(255, 238, 200, 0.78);
}

.chat-panel strong {
  color: #fff0c9;
  font-weight: 800;
}
</style>
