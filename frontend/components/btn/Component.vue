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
  background-color: #111827;
  border: 1px solid transparent;
  border-radius: .75rem;
  box-sizing: border-box;
  color: #FFFFFF;
  cursor: pointer;
  flex: 0 0 auto;
  font-family: "Inter var",ui-sans-serif,system-ui,-apple-system,system-ui,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
  font-size: 1.125rem;
  font-weight: 600;
  line-height: 1.5rem;
  padding: .65rem 1rem;
  text-align: center;
  text-decoration: none #6B7280 solid;
  text-decoration-thickness: auto;
  transition-duration: .2s;
  transition-property: background-color,border-color,color,fill,stroke;
  transition-timing-function: cubic-bezier(.4, 0, 0.2, 1);
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
  width: auto;
}

.btn:hover {
  background-color: #374151;
}

.btn:focus {
  box-shadow: none;
  outline: 2px solid transparent;
  outline-offset: 2px;
}

.btn:hover:disabled {
  background-color: #1e2942;
}

.btn:disabled{
    background-color: #0000002a;
    cursor: not-allowed;
}

@media (min-width: 768px) {
  .btn {
    padding: .6rem .95rem;
  }
}

</style>
