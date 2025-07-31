<template>
  <div id="app">
    <nav class="navbar">
      <div class="navbar-content">
        <router-link to="/">Home</router-link>
        <router-link to="/search">Search</router-link>
        <router-link to="/about">About</router-link>
      </div>
    </nav>
    <div class="content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
body {
  font-family: Arial, sans-serif;
  margin: 0;
  padding: 0;
  background-color: #ffffff;
  min-height: 100vh;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.navbar {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%; /* Full lățime */
  background-color: #007bff;
  padding: 15px 0; /* Padding vertical, zero lateral */
  z-index: 1000;
  display: flex;
  justify-content: center; /* Centrează conținutul */
  align-items: center;
}

.navbar-content {
  width: 100%;
  max-width: 1200px; /* Lățime maximă pentru conținut central, ajustabil */
  display: flex;
  justify-content: space-around; /* Link-uri distribuite egal pe lățime */
  align-items: center;
}

.navbar a {
  color: rgb(255, 255, 255);
  text-decoration: none;
  font-weight: bold;
  font-size: 1.1rem;
  padding: 5px 15px; /* Spațiu în jurul fiecărui link */
}

.navbar a:hover {
  text-decoration: underline;
}

.content {
  margin-top: 60px; /* Spațiu sub navbar */
  padding: 20px;
  flex-grow: 1; /* Ocupă spațiul rămas */
  color: black;
}
</style>