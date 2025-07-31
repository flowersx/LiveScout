<template>
  <div class="search-page">
    <h2>Filtrează mașinile</h2>
    <div class="filter-section">
      <div class="filter-group">
        <label for="filter-year">An:</label>
        <input id="filter-year" v-model.number="filterYear" type="number" placeholder="ex: 2020" />
      </div>
      <div class="filter-group">
        <label for="filter-brand">Brand:</label>
        <input id="filter-brand" v-model="filterBrand" placeholder="ex: BMW" />
      </div>
      <div class="filter-group">
        <label for="filter-model">Model:</label>
        <input id="filter-model" v-model="filterModel" placeholder="ex: Seria 3" />
      </div>
      <div class="filter-group">
        <label for="filter-bodytype">Caroserie:</label>
        <select id="filter-bodytype" v-model="filterBodyType">
          <option value="">Toate</option>
          <option value="SUV">SUV</option>
          <option value="COUPE">COUPE</option>
          <option value="SEDAN">SEDAN</option>
          <option value="HATCHBACK">HATCHBACK</option>
        </select>
      </div>
      <div class="filter-actions">
        <button @click="fetchCars" class="apply-button">Aplică</button>
        <button @click="resetFilters" class="reset-button">Resetează</button>
      </div>
    </div>
    <div class="car-results">
      <div v-if="loading" class="loading-message">
        <div class="spinner"></div> Se încarcă...
      </div>
      <div v-else-if="cars.length === 0" class="no-results-message">
        <p>😞 Niciun rezultat.</p>
        <button @click="resetFilters" class="reset-button-inline">Resetează</button>
      </div>
      <div v-else class="car-grid">
        <div v-for="car in cars" :key="car.id" class="car-card">
          <div class="car-image-placeholder">
            <img src="https://via.placeholder.com/300x200" :alt="car.displayName" />
          </div>
          <div class="car-info">
            <h3>{{ car.displayName }}</h3>
            <p class="car-specs">An: {{ car.year }} | Caroserie: {{ car.bodyType }}</p>
            <button class="view-details-button">Vezi Detalii</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'CarList',
  data() {
    return {
      cars: [],
      filterYear: null,
      filterBrand: '',
      filterModel: '',
      filterBodyType: '',
      loading: false,
    };
  },
  created() {
    this.fetchCars();
  },
  methods: {
    async fetchCars() {
      this.loading = true;
      try {
        const params = {
          year: this.filterYear,
          brand: this.filterBrand,
          model: this.filterModel,
          bodyType: this.filterBodyType,
        };
        const filteredParams = Object.fromEntries(
          Object.entries(params).filter(([_, value]) => value !== null && value !== '')
        );
        const response = await axios.get('https://localhost:7082/Cars', { params: filteredParams });
        this.cars = response.data;
      } catch (error) {
        console.error('Eroare:', error);
      } finally {
        this.loading = false;
      }
    },
    resetFilters() {
      this.filterYear = null;
      this.filterBrand = '';
      this.filterModel = '';
      this.filterBodyType = '';
      this.fetchCars();
    },
  },
};
</script>

<style scoped>
.search-page {
  padding: 20px;
}

.filter-section {
  margin-bottom: 20px;
}

.filter-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
}

.filter-group label {
  font-weight: 600;
  margin-bottom: 5px;
  color: #555;
}

.filter-group input,
.filter-group select {
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.filter-actions {
  display: flex;
  gap: 10px;
}

.apply-button, .reset-button {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.apply-button {
  background-color: #28a745;
  color: white;
}

.apply-button:hover {
  background-color: #218838;
}

.reset-button {
  background-color: #6c757d;
  color: white;
}

.reset-button:hover {
  background-color: #5a6268;
}

.car-results {
  margin-top: 20px;
}

.loading-message, .no-results-message {
  text-align: center;
  padding: 20px;
  background: #fff;
  border-radius: 8px;
}

.spinner {
  border: 4px solid #e0e0e0;
  border-top: 4px solid #007bff;
  border-radius: 50%;
  width: 30px;
  height: 30px;
  animation: spin 1s linear infinite;
  margin: 0 auto 10px;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

.car-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 20px;
}

.car-card {
  background: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 5px rgba(0,0,0,0.1);
}

.car-image-placeholder {
  height: 150px;
  background: #f8f9fa;
  display: flex;
  align-items: center;
  justify-content: center;
}

.car-image-placeholder img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.car-info {
  padding: 10px;
  text-align: center;
}

.car-info h3 {
  margin: 0 0 10px;
  font-size: 1.2rem;
}

.car-specs {
  color: #666;
  margin-bottom: 10px;
}

.view-details-button {
  background-color: #007bff;
  color: white;
  padding: 8px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.view-details-button:hover {
  background-color: #0056b3;
}

@media (max-width: 768px) {
  .filter-actions {
    flex-direction: column;
  }
  .apply-button, .reset-button {
    width: 100%;
    margin-bottom: 10px;
  }
}
</style>