<template>
    <select v-model="modelValue" :required="props.required" :disabled="props.disabled" class="custom-select">
        <option v-for="option in props.options" :key="option.value" :value="option.value">
            {{ option.label }}
        </option>
    </select>
</template>
<script setup lang="ts">
const modelValue = defineModel<string>();
const props = defineProps({
    options: {
        type: Array as PropType<Array<{ value: string; label: string }>>,
        required: true
    },
    required: {
        type: Boolean,
        default: false
    },
    disabled: {
        type: Boolean,
    }
})
</script>
<style scoped>
select {
    padding: 0.2rem 0.4rem;
    background-color: var(--color-background);    
    color: var(--color-text);
    border-color: var(--color-text);
    border-radius: .2rem;
}

option {
    background-color: var(--color-background);
    color: var(--color-text);
}
</style>