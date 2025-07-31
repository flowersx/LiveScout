<template>
  <div class="home-page">
    <div class="header-actions">
      <h2>Toate mașinile</h2>
      <button v-if="!showForm" @click="showForm = true" class="btn-add">Adaugă Mașină</button>
    </div>

    <CarForm
      v-if="showForm"
      :car="selectedCar"
      :is-editing="!!selectedCar"
      @submit="handleSubmit"
      @cancel="cancelForm"
    />

    <div v-if="loading">Încărcare...</div>
    <div v-else-if="cars.length === 0">Nu sunt mașini.</div>
    <div v-else class="car-grid">
      <div v-for="car in cars" :key="car.id" class="car-frame">
        <img :src="car.imageUrl || 'https://via.placeholder.com/300x200?text=Imagine+Masina'" :alt="car.displayName" class="car-image" />
        <div class="car-info">
          <h3>{{ car.displayName }}</h3>
          <p>Preț: {{ car.priceFormatted }}</p>
          <p>An: {{ car.year }}</p>
          <p>Km: {{ car.mileageFormatted }}</p>
          <p>Brand: {{ car.brand }}</p>
          <p>Model: {{ car.model }}</p>
          <p>Tip: {{ car.bodyType }}</p>
          <p>Combustibil: {{ car.fuelType }}</p>
          <p>Transmisie: {{ car.transmission }}</p>
          <p>Culoare: {{ car.color }}</p>
          <p>Disponibil: {{ car.isAvailable ? 'Da' : 'Nu' }}</p>
          <div class="car-actions">
            <button @click="editCar(car)" class="btn-edit">Editează</button>
            <button @click="deleteCar(car.id)" class="btn-delete">Șterge</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import CarForm from '@/components/CarForm.vue';

export default {
  name: 'HomeView',
  components: {
    CarForm
  },
  data() {
    return {
      cars: [],
      loading: false,
      showForm: false,
      selectedCar: null
    };
  },
  async created() {
    await this.loadCars();
  },
  methods: {
    async loadCars() {
      this.loading = true;
      try {
        const response = await axios.get('https://localhost:7056/Cars');
        this.cars = response.data;
      } catch (error) {
        console.error('Eroare la încărcarea mașinilor:', error);
        alert('Eroare la încărcarea mașinilor. Vă rugăm să încercați din nou.');
      } finally {
        this.loading = false;
      }
    },
    
    async handleSubmit(carData) {
      try {
        if (this.selectedCar) {
          // Update
          await axios.put(`https://localhost:7056/Cars/${this.selectedCar.id}`, {
            ...carData,
            id: this.selectedCar.id
          });
          alert('Mașina a fost actualizată cu succes!');
        } else {
          // Create
          await axios.post('https://localhost:7056/Cars', carData);
          alert('Mașina a fost adăugată cu succes!');
        }
        
        this.cancelForm();
        await this.loadCars();
      } catch (error) {
        console.error('Eroare la salvarea mașinii:', error);
        alert('Eroare la salvarea mașinii. Vă rugăm să încercați din nou.');
      }
    },

    editCar(car) {
      this.selectedCar = car;
      this.showForm = true;
    },

    async deleteCar(id) {
      if (!confirm('Sunteți sigur că doriți să ștergeți această mașină?')) return;
      
      try {
        await axios.delete(`https://localhost:7056/Cars/${id}`);
        alert('Mașina a fost ștearsă cu succes!');
        await this.loadCars();
      } catch (error) {
        console.error('Eroare la ștergerea mașinii:', error);
        alert('Eroare la ștergerea mașinii. Vă rugăm să încercați din nou.');
      }
    },

    cancelForm() {
      this.showForm = false;
      this.selectedCar = null;
    }
  }
};
</script>

<style scoped>
.home-page {
  padding: 20px;
  text-align: center;
}

.header-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.btn-add {
  background-color: #4CAF50;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.btn-add:hover {
  opacity: 0.9;
}

.car-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 20px;
  margin-top: 20px;
  justify-content: center;
}

.car-frame {
  border: 1px solid #ccc;
  padding: 15px;
  border-radius: 8px;
  background: #f8f9fa;
  box-shadow: 0 2px 5px rgba(0,0,0,0.1);
  max-width: 300px;
  transition: transform 0.2s;
}

.car-frame:hover {
  transform: translateY(-5px);
}

.car-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
  border-radius: 4px;
  margin-bottom: 10px;
}

.car-info h3 {
  margin: 0 0 10px;
  font-size: 1.2rem;
}

.car-info p {
  margin: 5px 0;
  color: #666;
  text-align: left;
}

.car-actions {
  display: flex;
  gap: 10px;
  margin-top: 15px;
  justify-content: center;
}

.btn-edit,
.btn-delete {
  padding: 8px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn-edit {
  background-color: #2196F3;
  color: white;
}

.btn-delete {
  background-color: #f44336;
  color: white;
}

.btn-edit:hover,
.btn-delete:hover {
  opacity: 0.9;
}

.loading, .no-results {
  padding: 20px;
  color: #666;
}

/* Pentru ecrane mari */
@media (min-width: 768px) {
  .car-grid {
    grid-template-columns: repeat(3, 1fr);
  }
}

/* Pentru mobil */
@media (max-width: 767px) {
  .car-grid {
    grid-template-columns: 1fr;
  }
  
  .header-actions {
    flex-direction: column;
    gap: 10px;
  }
  
  .car-frame {
    max-width: 100%;
  }
}
</style>