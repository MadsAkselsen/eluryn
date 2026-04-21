<script setup lang="ts">
defineEmits<{
  close: []
}>()
</script>

<template>
  <div class="panel-backdrop" role="presentation" @click.self="$emit('close')">
    <section class="panel" role="dialog" aria-modal="true" aria-labelledby="settings-title">
      <header>
        <h2 id="settings-title">Settings</h2>
        <button type="button" aria-label="Close settings" @click="$emit('close')">
          x
        </button>
      </header>

      <label>
        Ambience
        <select>
          <option>Classroom</option>
          <option>Library</option>
          <option>Rainy window</option>
        </select>
      </label>

      <label class="toggle">
        <input type="checkbox" checked />
        Timer sounds
      </label>
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
  gap: 1rem;
  padding: 1.25rem;
  color: #f8ecd1;
  background: rgba(33, 21, 14, 0.96);
  box-shadow: -20px 0 50px rgba(0, 0, 0, 0.35);
}

header {
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

label {
  display: grid;
  gap: 0.4rem;
  font-weight: 700;
}

select {
  border: 1px solid rgba(239, 204, 138, 0.35);
  border-radius: 6px;
  padding: 0.65rem;
  color: #f8ecd1;
  background: #2a1b12;
}

.toggle {
  grid-template-columns: auto 1fr;
  align-items: center;
}
</style>
