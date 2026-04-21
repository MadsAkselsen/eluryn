import { computed, ref } from 'vue'
import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', () => {
  const displayName = ref('Student')

  const initials = computed(() =>
    displayName.value
      .split(' ')
      .map((part) => part[0])
      .join('')
      .slice(0, 2)
      .toUpperCase(),
  )

  return {
    displayName,
    initials,
  }
})
