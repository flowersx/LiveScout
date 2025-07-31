import { createApp } from 'vue';
import App from './App.vue';
import router from './router/index.js'; // Importă router-ul

const app = createApp(App);
app.use(router); // Use router
app.mount('#app');