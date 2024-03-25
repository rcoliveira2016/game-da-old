<template>
  <div class="app">
    <LayoutDefaultHeaderComponent />
    <main>
      <slot />
    </main>
  </div>
</template>
<style>

main{
  padding: 0.6rem;
  padding-top: var(--spacing-lg);
}
.app{
  width: 100%;
  position: relative;
}
</style>