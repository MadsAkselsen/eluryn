import { computed, ref } from 'vue'
import { defineStore } from 'pinia'

export type AppView = 'landing' | 'room'
export type AppPanel = 'settings' | 'profile' | null

const backgroundImageModules = import.meta.glob('../assets/media/background-*.png', {
  eager: true,
  import: 'default',
})

const backgroundImages = Object.entries(backgroundImageModules)
  .map(([path, url]) => {
    const fileName = path.split('/').at(-1) ?? path

    return {
      name: fileName.replace('.png', '').replaceAll('-', ' '),
      url: url as string,
    }
  })
  .sort((a, b) => a.name.localeCompare(b.name, undefined, { numeric: true }))

export const useUiStore = defineStore('ui', () => {
  const currentView = ref<AppView>('landing')
  const activePanel = ref<AppPanel>(null)
  const activeBackgroundIndex = ref(0)

  const isLanding = computed(() => currentView.value === 'landing')
  const isRoom = computed(() => currentView.value === 'room')
  const hasOpenPanel = computed(() => activePanel.value !== null)
  const activeBackground = computed(
    () => backgroundImages[activeBackgroundIndex.value],
  )
  const backgroundImageStyle = computed(() => ({
    backgroundImage: `url(${activeBackground.value.url})`,
  }))

  function goToLanding() {
    currentView.value = 'landing'
    activePanel.value = null
  }

  function enterRoom() {
    currentView.value = 'room'
    activePanel.value = null
  }

  function openSettings() {
    activePanel.value = 'settings'
  }

  function openProfile() {
    activePanel.value = 'profile'
  }

  function closePanel() {
    activePanel.value = null
  }

  function selectNextBackground() {
    activeBackgroundIndex.value =
      (activeBackgroundIndex.value + 1) % backgroundImages.length
  }

  return {
    activePanel,
    activeBackground,
    closePanel,
    currentView,
    enterRoom,
    goToLanding,
    backgroundImages,
    backgroundImageStyle,
    hasOpenPanel,
    isLanding,
    isRoom,
    openProfile,
    openSettings,
    selectNextBackground,
  }
})
