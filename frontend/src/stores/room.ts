import { computed, ref } from 'vue'
import { defineStore } from 'pinia'

export const useRoomStore = defineStore('room', () => {
  const roomName = ref('Quiet Study Room')
  const participantCount = ref(3)
  const chatMessages = ref([
    {
      id: 'welcome',
      author: 'Eluryn',
      body: 'Welcome in. Settle down and start when you are ready.',
    },
  ])

  const roomSummary = computed(
    () => `${roomName.value} · ${participantCount.value} studying`,
  )

  return {
    chatMessages,
    participantCount,
    roomName,
    roomSummary,
  }
})
