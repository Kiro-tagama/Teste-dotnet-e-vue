
<script setup lang="ts">
defineProps<{
  key: string;
  title: string;
  openModal?: (event: Event) => void;
}>();
</script>

<template #actions>
  <v-card v-if="title === 'ALL'" class="card card-content-center" >
    <h3 class="category-title category-title-all">{{ title }}</h3>
  </v-card>
  <v-card v-else class="card">
    <v-btn class="options-button" 
      v-if="openModal" 
      @click.stop.prevent="openModal"
      icon="mdi-dots-vertical" 
      size="x-small"></v-btn>
    <img :src="`https://cdn.vuetifyjs.com/images/parallax/material.jpg`" alt="Categoria" class="category-image" />
    <h3 class="category-title">{{ title }}</h3>
  </v-card>
</template>

<style scoped>

.card{
  max-width: 465px;
  height: 167px;
}

.card-content-center {
  display: flex;
  justify-content: center;
  align-items: center;
}

.category-image {
  width: 100%;
  height: 120px;
  object-fit: cover;
  border-top: 6px;
}

.category-title {
  margin-top: 10px;
  font-size: 16px;
  font-weight: bold;
  padding: .5rem;
  margin: 0;
}

.category-title-all {
  text-align: center;
  margin: 0 auto;
}

.options-button {
  position: absolute;
  top: 8px;
  right: 8px;
  border: none;
  cursor: pointer;
  padding: 0;
  border: 100%;
  z-index: 2;
}
</style>