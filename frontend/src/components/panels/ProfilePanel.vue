<script setup lang="ts">
import { useUserStore } from '@/stores/user'

defineEmits<{
  close: []
}>()

const user = useUserStore()
</script>

<template>
  <div class="panel-backdrop" role="presentation" @click.self="$emit('close')">
    <section class="panel" role="dialog" aria-modal="true" aria-labelledby="profile-title">
      <header>
        <h2 id="profile-title">Profile</h2>
        <button type="button" aria-label="Close profile" @click="$emit('close')">
          x
        </button>
      </header>

      <div class="avatar" aria-hidden="true">{{ user.initials }}</div>
      <p>{{ user.displayName }}</p>
      <span>Local session</span>
    </section>
  </div>
</template>

<style scoped>
.panel-backdrop {
  position: fixed;
  inset: 0;
  z-index: 10;
  display: grid;
  justify-items: end;
  background: rgba(13, 8, 6, 0.42);
}

.panel {
  width: min(100%, 360px);
  min-height: 100%;
  display: grid;
  align-content: start;
  justify-items: start;
  gap: 1rem;
  padding: 1.25rem;
  color: #f8ecd1;
  background: rgba(33, 21, 14, 0.96);
  box-shadow: -20px 0 50px rgba(0, 0, 0, 0.35);
}

header {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 1rem;
}

h2 {
  color: #fff0c9;
  font-size: 1.35rem;
  font-weight: 800;
}

button {
  width: 34px;
  height: 34px;
  border: 1px solid rgba(239, 204, 138, 0.35);
  border-radius: 6px;
  color: #f8ecd1;
  background: rgba(73, 47, 25, 0.72);
  cursor: pointer;
  font-weight: 800;
}

.avatar {
  width: 72px;
  height: 72px;
  display: grid;
  place-items: center;
  border: 1px solid rgba(239, 204, 138, 0.38);
  border-radius: 50%;
  color: #2a1202;
  background: linear-gradient(#efb34d, #a8510b);
  font-weight: 900;
}

p {
  color: #fff0c9;
  font-size: 1.1rem;
  font-weight: 800;
}

span {
  color: rgba(255, 238, 200, 0.72);
}
</style>
