<script setup lang="ts">
const modelValue = defineModel<string>();
const props = defineProps({
    required: {
        type: Boolean,
        default: false
    }
})
</script>
<template>
    <input type="text" v-model="modelValue" :required="props.required" />
</template>
<style scoped>
input {
    width: 100%;
    background-color: #ffffff2a;
    border: 1px solid #cacacab9;
    padding: 0.2rem;
    color: var(--color-text);
}
</style>