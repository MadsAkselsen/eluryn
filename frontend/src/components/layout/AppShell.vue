<script setup lang="ts">
import { onMounted } from 'vue'

import ProfilePanel from '@/components/panels/ProfilePanel.vue'
import SettingsPanel from '@/components/panels/SettingsPanel.vue'
import { useUiStore } from '@/stores/ui'
import LandingView from '@/views/LandingView.vue'
import RoomView from '@/views/RoomView.vue'

const ui = useUiStore()

onMounted(() => {
  const basePath = import.meta.env.BASE_URL

  if (window.location.pathname !== basePath) {
    window.history.replaceState({}, '', basePath)
  }
})
</script>

<template>
  <div class="app-shell">
    <LandingView v-if="ui.currentView === 'landing'" />
    <RoomView v-else />

    <SettingsPanel
      v-if="ui.activePanel === 'settings'"
      @close="ui.closePanel"
    />
    <ProfilePanel
      v-if="ui.activePanel === 'profile'"
      @close="ui.closePanel"
    />

    <button
      v-if="ui.backgroundImages.length > 1"
      type="button"
      class="background-switcher"
      :aria-label="`Switch background image. Current: ${ui.activeBackground.name}`"
      :title="`Current: ${ui.activeBackground.name}`"
      @click="ui.selectNextBackground"
    >
      <svg viewBox="0 0 24 24" aria-hidden="true">
        <path
          d="M4 5.5A2.5 2.5 0 0 1 6.5 3h11A2.5 2.5 0 0 1 20 5.5v13a2.5 2.5 0 0 1-2.5 2.5h-11A2.5 2.5 0 0 1 4 18.5v-13Zm2 0v8.88l3.1-3.1a1.25 1.25 0 0 1 1.77 0l1.88 1.88 4.5-4.5a1.25 1.25 0 0 1 .75-.35V5.5a.5.5 0 0 0-.5-.5h-11a.5.5 0 0 0-.5.5Zm0 13a.5.5 0 0 0 .5.5h11a.5.5 0 0 0 .5-.5v-7.3l-4.37 4.37a1.25 1.25 0 0 1-1.77 0l-1.88-1.88L6 17.67v.83ZM9 8a1.5 1.5 0 1 1 3 0A1.5 1.5 0 0 1 9 8Z"
        />
      </svg>
    </button>
  </div>
</template>

<style scoped>
.app-shell {
  min-height: 100dvh;
  isolation: isolate;
}

.background-switcher {
  position: fixed;
  right: 0.75rem;
  bottom: 0.75rem;
  z-index: 20;
  width: 40px;
  height: 40px;
  display: grid;
  place-items: center;
  border: 1px solid rgba(255, 232, 188, 0.38);
  border-radius: 8px;
  color: #fff0c9;
  background: rgba(21, 15, 11, 0.78);
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.32);
  cursor: pointer;
  backdrop-filter: blur(6px);
}

.background-switcher svg {
  width: 21px;
  height: 21px;
  fill: currentColor;
}

.background-switcher:hover {
  filter: brightness(1.12);
}
</style>
