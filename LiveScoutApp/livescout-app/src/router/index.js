import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue'; // Dacă ai Home în views
import CarList from '../components/CarList.vue'; // Importă din components
import AboutView from '../views/AboutView.vue'; // Dacă ai About în views

const routes = [
  { path: '/', name: 'Home', component: HomeView },
  { path: '/search', name: 'Search', component: CarList }, // Încarcă CarList la /search
  { path: '/about', name: 'About', component: AboutView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;