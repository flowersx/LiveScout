<template>
  <div class="page-wrapper">
    <header class="app-header">
      <h1>Live Scout - Găsește-ți Mașina Ideală</h1>
      <p>Piața ta online de vânzări auto</p>
    </header>

    <div class="main-content">
      <aside class="filters-sidebar">
        <h2>Filtrează mașinile</h2>
        <div class="filter-group">
          <label for="filter-year">An:</label>
          <input
            id="filter-year"
            v-model.number="filterYear"
            type="number"
            placeholder="ex: 2020"
          />
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
            <option value="">Toate Caroseriile</option>
            <option value="SUV">SUV</option>
            <option value="COUPE">COUPE</option>
            <option value="SEDAN">SEDAN</option>
            <option value="HATCHBACK">HATCHBACK</option>
          </select>
        </div>
        <div class="filter-actions">
          <button @click="fetchCars" class="apply-button">Aplică Filtre</button>
          <button @click="resetFilters" class="reset-button">Resetează</button>
        </div>
      </aside>

      <aside class="car-results">
        <div v-if="loading" class="loading-message">
          <div class="spinner"></div>
          Se încarcă mașinile...
        </div>
        <div v-else-if="cars.length === 0" class="no-results-message">
          <p>😞 Ne pare rău, nu s-au găsit mașini conform filtrelor tale.</p>
          <button @click="resetFilters" class="reset-button-inline">Resetează filtrele</button>
        </div>
        <div v-else class="car-grid">
          <div v-for="car in cars" :key="car.id" class="car-card">
            <div class="car-image-placeholder">
              <img
                src="https://via.placeholder.com/300x200?text=Imagine+Masina"
                :alt="car.brand + ' ' + car.model"
              />
            </div>
            <div class="car-info">
              <h3>{{ car.brand }} {{ car.model }}</h3>
              <p class="car-specs">
                <span>An: {{ car.year }}</span>
                <span> | </span>
                <span>Caroserie: {{ car.bodyType }}</span>
              </p>
              <button class="view-details-button">Vezi Detalii</button>
            </div>
          </div>
        </div>
      </aside>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

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
    }
  },
  created() {
    this.fetchCars()
  },
  methods: {
    async fetchCars() {
      this.loading = true
      try {
        const params = {
          year: this.filterYear,
          brand: this.filterBrand,
          model: this.filterModel,
          bodyType: this.filterBodyType,
        }

        const filteredParams = Object.fromEntries(
          Object.entries(params).filter(([_, value]) => value !== null && value !== ''),
        )

        const response = await axios.get('https://localhost:7082/Cars', {
          params: filteredParams,
        })

        this.cars = response.data
      } catch (error) {
        console.error('Eroare la preluarea mașinilor:', error)
      } finally {
        this.loading = false
      }
    },
    resetFilters() {
      this.filterYear = null
      this.filterBrand = ''
      this.filterModel = ''
      this.filterBodyType = ''
      this.fetchCars()
    },
  },
}
</script>

<style scoped>
body {
  margin: 0;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background-color: #f0f2f5;
  color: #333;
}

.page-wrapper {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  max-width: 100%;
  background-color: #f0f2f5;
}

/* Antetul aplicației */
.app-header {
  background-color: #007bff;
  color: white;
  padding: 30px 20px;
  text-align: center;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.app-header h1 {
  margin: 0 0 10px 0;
  font-size: 2.5rem;
  letter-spacing: 0.5px;
}

.app-header p {
  margin: 0;
  font-size: 1.1rem;
  opacity: 0.9;
}

.main-content {
  display: auto;
  flex-grow: 1;
  margin: 30px 0;
  padding: 0 20px;
  width: 100%;
  max-width: none;
  margin-left: auto;
  margin-right: auto;
}

.filters-sidebar {
  background-color: #ffffff;
  padding: 25px;
  border-radius: 10px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
  flex-shrink: 0;
  width: 100%;
  align-self: flex-start;
  position: sticky;
  top: 20px;
  max-height: calc(100vh - 60px);
  overflow-y: auto;
}

.filters-sidebar h2 {
  color: #007bff;
  margin-top: 0;
  margin-bottom: 25px;
  text-align: center;
  font-size: 1.8rem;
}

.filter-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
}

.filter-group label {
  font-weight: 600;
  margin-bottom: 8px;
  color: #555;
  font-size: 0.95rem;
}

.filters-sidebar input[type='number'],
.filters-sidebar input[type='text'],
.filters-sidebar select {
  width: 100%;
  padding: 12px;
  border: 1px solid #e0e0e0;
  border-radius: 6px;
  font-size: 1rem;
  box-sizing: border-box;
  transition:
    border-color 0.2s ease,
    box-shadow 0.2s ease;
}

.filters-sidebar input:focus,
.filters-sidebar select:focus {
  border-color: #007bff;
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.25);
  outline: none;
}

.filter-actions {
  display: flex;
  gap: 10px;
  margin-top: 30px;
  justify-content: space-between;
}

.apply-button,
.reset-button,
.reset-button-inline,
.view-details-button {
  padding: 12px 20px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: 600;
  transition:
    background-color 0.3s ease,
    transform 0.2s ease;
}

.apply-button {
  background-color: #28a745; /* Verde mai închis */
  color: white;
  flex-grow: 1;
}

.apply-button:hover {
  background-color: #218838;
  transform: translateY(-2px);
}

.reset-button {
  background-color: #6c757d; /* Gri */
  color: white;
  flex-grow: 1;
}

.reset-button:hover {
  background-color: #5a6268;
  transform: translateY(-2px);
}

.reset-button-inline {
  background: none;
  color: #007bff;
  border: 1px solid #007bff;
  padding: 8px 15px;
  font-size: 0.9rem;
  margin-top: 15px;
  display: inline-block;
  border-radius: 5px;
}
.reset-button-inline:hover {
  background-color: #e6f2ff;
  text-decoration: underline;
}

.car-results {
  flex-grow: 1;
  display: flex;
  padding-top: 100px;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 300px;
}

.loading-message,
.no-results-message {
  text-align: center;
  padding: 40px;
  font-size: 1.3rem;
  color: #666;
  background-color: #ffffff;
  border-radius: 10px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
  width: 80%;
  max-width: 500px;
  margin: 50px auto; /* Centrează și mesajele */
  display: flex;
  flex-direction: column;
  align-items: center;
}

/* Spinner de încărcare */
.spinner {
  border: 4px solid rgba(0, 123, 255, 0.2);
  border-top: 4px solid #007bff;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  animation: spin 1s linear infinite;
  margin-bottom: 20px;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

/* Grila de mașini */
.car-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr)); /* Rămânem cu o grilă responsivă */
  gap: 25px;
  width: 100%; /* Ocupă toată lățimea disponibilă */
}

.car-card {
  background: white;
  border-radius: 10px;
  overflow: hidden; /* Asigură că imaginea nu depășește colțurile rotunjite */
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
  transition:
    transform 0.3s ease,
    box-shadow 0.3s ease;
  display: flex;
  flex-direction: column; /* Conținutul cardului aranjat pe verticală */
}

.car-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 12px 25px rgba(0, 0, 0, 0.15);
}

.car-image-placeholder {
  width: 100%;
  height: 200px; /* Înălțime fixă pentru imagine */
  background-color: #e9ecef; /* Gri deschis pentru placeholder */
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 0.9rem;
  color: #6c757d;
  overflow: hidden; /* Pentru a ascunde ce depășește */
}

.car-image-placeholder img {
  width: 100%;
  height: 100%;
  object-fit: cover; /* Asigură că imaginea acoperă spațiul fără a se deforma */
  display: block;
}

.car-info {
  padding: 20px;
  display: flex;
  flex-direction: column;
  flex-grow: 1; /* Permite info să ocupe spațiul rămas */
}

.car-info h3 {
  color: #333;
  margin-top: 0;
  margin-bottom: 10px;
  font-size: 1.5rem;
  line-height: 1.3;
}

.car-specs {
  color: #666;
  font-size: 0.9rem;
  margin-bottom: 15px;
  flex-grow: 1;
}

.car-specs span {
  font-weight: 500;
}

.view-details-button {
  background-color: #007bff;
  color: white;
  width: 100%;
  align-self: flex-end;
}

.view-details-button:hover {
  background-color: #0056b3;
  transform: translateY(-2px);
}

@media (max-width: 992px) {
  .main-content {
    flex-direction: column;
    margin: 20px auto;
  }

  .filters-sidebar {
    width: 100%;
    position: static;
    max-height: unset;
    margin-bottom: 20px;
  }

  .filter-actions {
    flex-direction: column;
  }

  .apply-button,
  .reset-button {
    width: 100%;
  }

  .car-results {
    padding: 0;
  }

  .car-grid {
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  }
}

@media (max-width: 576px) {
  .app-header h1 {
    font-size: 2rem;
  }
  .app-header p {
    font-size: 1rem;
  }
  .car-card {
    min-width: unset;
  }
  .car-results,
  .loading-message,
  .no-results-message {
    width: 100%;
    box-shadow: none;
  }
  .loading-message,
  .no-results-message {
    padding: 20px;
  }
}
</style>
