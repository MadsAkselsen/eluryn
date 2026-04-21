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
      class="dev-background-switcher"
      :title="`Current: ${ui.activeBackground.name}`"
      @click="ui.selectNextBackground"
    >
      BG
    </button>
  </div>
</template>

<style scoped>
.app-shell {
  min-height: 100dvh;
  isolation: isolate;
}

.dev-background-switcher {
  position: fixed;
  right: 0.75rem;
  bottom: 0.75rem;
  z-index: 20;
  width: 42px;
  height: 32px;
  border: 1px solid rgba(255, 232, 188, 0.38);
  border-radius: 6px;
  color: #fff0c9;
  background: rgba(21, 15, 11, 0.78);
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.32);
  cursor: pointer;
  font-size: 0.75rem;
  font-weight: 800;
}

.dev-background-switcher:hover {
  filter: brightness(1.12);
}
</style>
