<script setup lang="ts">
import type { Products } from "~/interfaces/products";

const route = useRoute();
const {id} = route.params;

const urlBase: string = "http://localhost:5054";
let url = id == "all"? urlBase+"/api/products" : urlBase + "/api/products/category/" + id

const { data: products } = useFetch<Products[]>(url);

</script>

<template>
  <h1>pagina list products</h1>
  <div>Totel de produtos: {{ products?.length }}</div>
  <ul>
    <li v-for="product in products" :key="product.id">
      <router-link :to="`/product/${product.id}`">{{ product.name }}</router-link>
    </li>
  </ul>
</template>