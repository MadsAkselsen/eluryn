<script setup lang="ts">
import { usePomodoroStore } from '@/stores/pomodoro'
import { useUiStore } from '@/stores/ui'

const pomodoro = usePomodoroStore()
const ui = useUiStore()

function startStudying() {
  pomodoro.startSession()
  ui.enterRoom()
}
</script>

<template>
  <main
    class="landing-view"
    :style="ui.backgroundImageStyle"
  >
    <section class="welcome-panel" aria-labelledby="welcome-title">
      <div class="welcome-content">
        <h1 id="welcome-title">Welcome to Eluryn</h1>
        <p>Focus with others</p>
        <strong>25:00</strong>

        <button type="button" @click="startStudying">Start focusing</button>
      </div>
    </section>
  </main>
</template>

<style scoped>
.landing-view {
  min-height: 100dvh;
  display: grid;
  place-items: center;
  padding: clamp(1rem, 6vw, 3rem);
  background-color: #271b16;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}

.welcome-panel {
  width: min(100%, 320px);
  min-height: min(78dvh, 520px);
  display: grid;
  align-items: start;
  padding: clamp(2.75rem, 9vh, 4.5rem) 1.25rem 1.25rem;
  overflow: hidden;
  border: 1px solid rgba(221, 174, 89, 0.4);
  border-radius: 8px;
  background:
    linear-gradient(
      rgba(35, 22, 14, 0.2),
      rgba(35, 22, 14, 0.04) 52%,
      rgba(28, 16, 10, 0.2)
    ),
    rgba(18, 11, 8, 0.12);
  box-shadow:
    0 28px 70px rgba(0, 0, 0, 0.42),
    inset 0 1px 0 rgba(255, 224, 154, 0.16);
  color: #f7e8c3;
  text-align: center;
}

.welcome-content {
  display: grid;
  justify-items: center;
  gap: 0.35rem;
  text-shadow: 0 2px 10px rgba(0, 0, 0, 0.75);
}

h1 {
  color: #fff0c9;
  font-family: Georgia, 'Times New Roman', serif;
  font-size: clamp(1.35rem, 5vw, 1.75rem);
  font-weight: 500;
  line-height: 1.1;
}

p {
  color: rgba(255, 237, 199, 0.86);
  font-size: 0.85rem;
}

strong {
  margin-block: 0.2rem 0.45rem;
  color: #ffe9b2;
  font-size: clamp(1.8rem, 8vw, 2.4rem);
  font-weight: 400;
  line-height: 1;
}

button {
  width: min(100%, 220px);
  min-height: 36px;
  border: 1px solid #b56812;
  border-radius: 6px;
  color: #2a1202;
  background:
    linear-gradient(#f5b647, #b75e0d 64%, #803503),
    #c87515;
  box-shadow:
    inset 0 1px 0 rgba(255, 242, 180, 0.55),
    0 4px 10px rgba(0, 0, 0, 0.28);
  cursor: pointer;
  font-weight: 800;
}

button:hover {
  filter: brightness(1.08);
}

@media (min-width: 760px) {
  .welcome-panel {
    width: 390px;
    min-height: 560px;
    padding-top: 4.5rem;
  }
}
</style>
