<template>
  <div class="car-form">
    <form @submit.prevent="submitForm" class="form-container">
      <div class="form-group">
        <label for="brand">Brand:</label>
        <input id="brand" v-model="carData.brand" required maxlength="50" />
      </div>

      <div class="form-group">
        <label for="model">Model:</label>
        <input id="model" v-model="carData.model" required maxlength="50" />
      </div>

      <div class="form-group">
        <label for="year">An:</label>
        <input id="year" type="number" v-model.number="carData.year" required min="1900" max="2030" />
      </div>

      <div class="form-group">
        <label for="bodyType">Tip caroserie:</label>
        <select id="bodyType" v-model="carData.bodyType" required>
          <option value="Sedan">Sedan</option>
          <option value="SUV">SUV</option>
          <option value="Coupe">Coupe</option>
          <option value="Hatchback">Hatchback</option>
          <option value="Break">Break</option>
        </select>
      </div>

      <div class="form-group">
        <label for="price">Preț:</label>
        <input id="price" type="number" v-model.number="carData.price" required min="0.01" step="0.01" />
      </div>

      <div class="form-group">
        <label for="mileage">Kilometraj:</label>
        <input id="mileage" type="number" v-model.number="carData.mileage" required min="0" />
      </div>

      <div class="form-group">
        <label for="fuelType">Tip combustibil:</label>
        <select id="fuelType" v-model="carData.fuelType" required>
          <option value="Benzina">Benzină</option>
          <option value="Diesel">Diesel</option>
          <option value="Hybrid">Hybrid</option>
          <option value="Electric">Electric</option>
        </select>
      </div>

      <div class="form-group">
        <label for="transmission">Transmisie:</label>
        <select id="transmission" v-model="carData.transmission" required>
          <option value="Manuala">Manuală</option>
          <option value="Automata">Automată</option>
        </select>
      </div>

      <div class="form-group">
        <label for="color">Culoare:</label>
        <input id="color" v-model="carData.color" required maxlength="30" />
      </div>

      <div class="form-group">
        <label for="isAvailable">Disponibil:</label>
        <input id="isAvailable" type="checkbox" v-model="carData.isAvailable" />
      </div>

      <div class="form-group">
        <label for="image">Imagine:</label>
        <input 
          id="image" 
          type="file" 
          @change="handleImageUpload" 
          accept="image/*"
        />
        <img v-if="imagePreview" :src="imagePreview" alt="Preview" class="image-preview" />
      </div>

      <div class="form-actions">
        <button type="submit" class="btn-submit">{{ isEditing ? 'Actualizează' : 'Adaugă' }}</button>
        <button type="button" @click="$emit('cancel')" class="btn-cancel">Anulează</button>
      </div>
    </form>
  </div>
</template>


<script>
export default {
  name: 'CarForm',
  props: {
    car: {
      type: Object,
      default: null
    },
    isEditing: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      carData: {
        brand: '',
        model: '',
        year: new Date().getFullYear(),
        bodyType: 'Sedan',
        price: 0,
        mileage: 0,
        fuelType: 'Benzina',
        transmission: 'Manuala',
        color: '',
        isAvailable: true,
        imageUrl: ''
      },
      imagePreview: null
    }
  },
  created() {
    if (this.car) {
      this.carData = { ...this.car }
      this.imagePreview = this.car.imageUrl
    }
  },
  methods: {
    async handleImageUpload(event) {
      const file = event.target.files[0]
      if (!file) return

      try {
        const base64String = await this.convertToBase64(file)
        this.carData.imageUrl = base64String
        this.imagePreview = base64String
      } catch (error) {
        console.error('Error converting image:', error)
      }
    },
    convertToBase64(file) {
      return new Promise((resolve, reject) => {
        const reader = new FileReader()
        reader.onload = () => resolve(reader.result)
        reader.onerror = error => reject(error)
        reader.readAsDataURL(file)
      })
    },
    submitForm() {
      this.$emit('submit', this.carData)
    }
  }
}
</script>;

<style scoped>
.car-form {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
}

.form-container {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.form-group label {
  font-weight: bold;
}

.form-group input,
.form-group select {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.form-group input[type="checkbox"] {
  width: 20px;
  height: 20px;
}

.image-preview {
  max-width: 200px;
  margin-top: 10px;
}

.form-actions {
  display: flex;
  gap: 10px;
  justify-content: flex-end;
  margin-top: 20px;
}

.btn-submit,
.btn-cancel {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.btn-submit {
  background-color: #4CAF50;
  color: white;
}

.btn-cancel {
  background-color: #f44336;
  color: white;
}

.btn-submit:hover,
.btn-cancel:hover {
  opacity: 0.9;
}
</style>