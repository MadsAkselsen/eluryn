import { computed, onScopeDispose, ref } from 'vue'
import { defineStore } from 'pinia'

type TimerMode = 'focus' | 'shortBreak'

const FOCUS_SECONDS = 25 * 60
const SHORT_BREAK_SECONDS = 5 * 60

export const usePomodoroStore = defineStore('pomodoro', () => {
  const mode = ref<TimerMode>('focus')
  const secondsRemaining = ref(FOCUS_SECONDS)
  const isRunning = ref(false)
  let intervalId: ReturnType<typeof setInterval> | null = null

  const totalSeconds = computed(() =>
    mode.value === 'focus' ? FOCUS_SECONDS : SHORT_BREAK_SECONDS,
  )

  const minutes = computed(() =>
    Math.floor(secondsRemaining.value / 60)
      .toString()
      .padStart(2, '0'),
  )

  const seconds = computed(() =>
    (secondsRemaining.value % 60).toString().padStart(2, '0'),
  )

  const displayTime = computed(() => `${minutes.value}:${seconds.value}`)
  const progress = computed(
    () => 1 - secondsRemaining.value / totalSeconds.value,
  )

  function stopTicking() {
    if (!intervalId) return

    clearInterval(intervalId)
    intervalId = null
  }

  function setMode(nextMode: TimerMode) {
    mode.value = nextMode
    secondsRemaining.value =
      nextMode === 'focus' ? FOCUS_SECONDS : SHORT_BREAK_SECONDS
  }

  function advanceMode() {
    setMode(mode.value === 'focus' ? 'shortBreak' : 'focus')
  }

  function start() {
    if (intervalId) return

    isRunning.value = true
    intervalId = setInterval(() => {
      if (secondsRemaining.value > 0) {
        secondsRemaining.value -= 1
        return
      }

      advanceMode()
    }, 1000)
  }

  function pause() {
    isRunning.value = false
    stopTicking()
  }

  function reset() {
    pause()
    setMode('focus')
  }

  function skip() {
    pause()
    advanceMode()
    start()
  }

  function startSession() {
    setMode('focus')
    start()
  }

  onScopeDispose(stopTicking)

  return {
    displayTime,
    isRunning,
    mode,
    progress,
    reset,
    skip,
    start,
    startSession,
    pause,
  }
})
