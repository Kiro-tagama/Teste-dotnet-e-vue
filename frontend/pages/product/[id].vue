<script setup lang="ts">
import type { Products } from "~/interfaces/products";
import type { Categories } from "~/interfaces/categories";

const route = useRoute();
const { id } = route.params;

const urlBase = "http://localhost:5054";
const { data: product } = await useAsyncData(`product-${id}`, () => 
  $fetch<Products>(`${urlBase}/api/products/${id}`)
);

const { data: category } = await useAsyncData(`category-${product.value?.categoryId}`, () => 
  product.value?.categoryId ? $fetch<Categories>(`${urlBase}/api/categories/${product.value.categoryId}`) : null
);
</script>

<template>
  <h1>Página Products</h1>
  <br />
  
  <h3>Sobre o Produto</h3>
  <div v-if="product">{{ product }}</div>
  <div v-else>Carregando produto...</div>
  
  <br />
  
  <h3>Sobre a Categoria</h3>
  <div v-if="category">{{ category }}</div>
  <div v-else>Carregando categoria...</div>
</template>
