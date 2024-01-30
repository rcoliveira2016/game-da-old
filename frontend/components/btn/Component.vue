<script setup lang="ts">
const props = defineProps({
  disabled: {
    type: Boolean,
    default: false,
  },
  loading: {
    type: Boolean,
    default: false,
  },
  size: {
    type: String,
    default: "md",
  },
  variant: {
    type: String,
    default: "primary",
  },
});
</script>

<template>
  <button
    :disabled="props.disabled"
    :class="`btn btn-${props.size} btn-${props.variant}`"
  >
    <slot />
  </button>
</template>
<style scoped>
.btn {
  padding: 0.1rem 0.3rem;
  border-radius: 0.25rem;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
  border: 1px solid transparent;
  font-size: 1rem;
  line-height: 1.5;
  font-weight: 500;
  text-align: center;
  text-decoration: none;
}
.btn:hover {
  transform: scale(1.05);  
}
</style>
