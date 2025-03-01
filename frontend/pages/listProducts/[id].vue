<script setup lang="ts">
import type { Products } from "~/interfaces/products";
import { urls } from '~/composables/apiConfig';

const route = useRoute();
const {id} = route.params;

let url = id == "all"? urls.products : urls.productCategory + id

const { data: products } = await useAsyncData<Products[]>(`products`, () =>
  $fetch<Products[]>(url)
);

const inputProduct = ref("");
const productsNames = computed(() => products.value?.map(i => i.name) || []);
</script>

<template>
  <h1 class="d-flex align-center">
    <v-btn variant="outlined" to="/" icon="mdi-arrow-left" class="mr-4" size="small"></v-btn>
    Página list products
  </h1>
  <br>
  <div class="d-flex flex-row" style="gap:.5rem" >
    <v-autocomplete
      label="Buscar"
      v-model="inputProduct"
      :items="productsNames"
    ></v-autocomplete>
    <v-btn height="56" :to="`/product/new`">ADD</v-btn>
  </div>
  <div v-if="products">Totel de produtos: {{ products?.length }}</div>
  <br>

  <!-- <div v-if="products == null">
    Carregando ou lista vazia
    {{ products?.length }}
    {{ typeof products }}
  </div> -->
  <v-list-item
    v-if="products"
    v-for="product in products"
    :key="product.id"
    :value="product.id"
    :to="`/product/${product.id}`"
    active-class="primary"
    class="py-3"
  >
    <div class="d-flex justify-start align-center ga-4">
      <v-img
        max-width="50"
        height="50"
        cover
        class="rounded"
        src="https://cdn.vuetifyjs.com/images/parallax/material.jpg"
        ></v-img>
      <div class="flex " style="flex: 1;">
        <h3>{{ product.name }}</h3>
        <span class="d-flex ga-2">
          <p>R$ {{ product.price.toFixed(2) }}</p>
          <p>-</p>
          <p :style="{ color: product.quantity <= 10 ? '#f56' : 'inherit' }">Quantidade: {{ product.quantity }}</p>
        </span>
      </div>
    </div>
  </v-list-item>
  <div v-else>
    <br>
    <v-alert type="info" variant="outlined">
      <div class="d-flex flex-row justify-space-between align-center">
        <div>
          <h3>Sem produtos</h3>
          <p>Crie um novo produto para começar.</p>
        </div>
        <v-btn :to="`/product/new`" color="primary">ADICIONAR PRODUTO</v-btn>
      </div>
    </v-alert>
  </div>
  
</template>