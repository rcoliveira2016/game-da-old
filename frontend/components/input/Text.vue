<script setup lang="ts">
const modelValue = defineModel<string>();
const props = defineProps({
    required: {
        type: Boolean,
        default: false
    },
    disabled: {
        type: Boolean,
    }
})
</script>
<template>
    <input type="text" v-model="modelValue" :required="props.required" :disabled="props.disabled" />
</template>
<style scoped>
input {
    width: 100%;
    background-color: #ffffff2a;
    border: 1px solid #cacacab9;
    padding: 0.2rem;
    color: var(--color-text);
}
input:disabled{
    background-color: #0000002a;
    cursor: not-allowed;
}
</style>