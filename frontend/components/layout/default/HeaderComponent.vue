<template>
  <header class="layout-header">
    <nav>
      <h1 class="title">Jogo da velha</h1>
      <ul>
        <li><NuxtLink to="/about">sobre</NuxtLink></li>
        <li><NuxtLink to="/online">online</NuxtLink></li>
      </ul>
    </nav>
  </header>
</template>
<style scoped>
.layout-header {
  background: #444444;
  color: #fff;
  display: flex;
  align-items: center;
  padding: var(--spacing-md) 0;
  box-shadow: var(--shadow-1);
}
.layout-header nav {
  display: flex;
}
.layout-header nav h1 {
  padding-left: var(--spacing-md);
  font-size: 1.5rem;
}

.layout-header nav ul {
  margin-left: var(--spacing-md);
  list-style: none;
  display: flex;
  gap: var(--spacing-sm);
}

.layout-header nav ul li {
  height: 100%;
  text-align: center;
  display: flex;
  align-items: center;
  padding: 0 0.3rem;
}
.layout-header nav ul li a {
  vertical-align: bottom;
}
.layout-header nav ul li:hover {
  background-color: #00000017;
}
.layout-header nav ul li:hover a {
  text-decoration: none;
}
</style>
