<template>
  <div id="app">
    <CarList />
  </div>
</template>

<script>
import CarList from './components/CarList.vue'

export default {
  name: 'App',
  components: {
    CarList, // Înregistrează componenta pentru a putea fi folosită în template
  },
}
</script>

<style>
/* Stiluri globale pentru aplicație */
body {
  font-family: Arial, sans-serif;
  margin: 0;
  padding: 0;
  background-color: #f0f2f5;
  min-height: 100vh;
}

#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  min-height: 100vh;
  display: flex;
  max-width: none !important;
  width: 100%;
  flex-direction: column;
}
</style>
