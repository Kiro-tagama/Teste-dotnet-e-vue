<script setup lang="ts">
import { watchEffect, watch, ref, computed } from "vue";
import { useRoute, useRouter } from "vue-router";
import { urls, customFetch } from "~/composables/apiConfig";
import type { Products } from "~/interfaces/products";
import type { Categories } from "~/interfaces/categories";

const route = useRoute();
const router = useRouter();
const { id } = route.params;

const product = ref<Products | null>(null);
const category = ref<Categories | Categories[] | null>(null);

// Buscar produto se não for um novo
if (id !== "new") {
  product.value = await $fetch<Products>(`${urls.products}${id}`);
}

// Criar novo produto
watchEffect(async () => {
  if (id === "new") {
    product.value = {
      id: "",
      name: "",
      description: "",
      price: 0,
      quantity: 0,
      categoryId: "",
      createdAt: "",
      updatedAt: "",
    };
    category.value = await $fetch<Categories[]>(urls.categories);
  }
});

// Observar mudanças na categoria do produto
watch(
  () => product.value?.categoryId,
  async (newCategoryId) => {
    if (newCategoryId) {
      category.value = await $fetch<Categories>(`${urls.categories}${newCategoryId}`);
    }
  }
);

const categoryNames = computed(() =>
  Array.isArray(category.value)
    ? category.value.map((i: Categories) => i.name)
    : []
);

const saveProduct = async () => {
  if (!product.value?.name) {
    alert("O nome do produto é obrigatório!");
    return;
  }

  const data = {
    name: product.value.name,
    description: product.value.description || "",
    price: product.value.price.toFixed(2),
    quantity: product.value.quantity.toFixed(0),
    updatedAt: new Date().toISOString(),
  };

  // pega o id da categoria com base no nome
  const categoryId = 
    Array.isArray(category.value) && 
    category.value?.find((cat) => cat.name === product.value?.categoryId)?.id

  try {
    if (id !== "new") {
      // console.log("Atualizado:", data);
      await customFetch("PUT", `${urls.products}${id}`, {
        id: product.value.id,
        ...data
      })
      .then(res=> console.log(res.status))
      .catch(err => console.log(err));
    } else {
      await customFetch("POST", urls.products, {
        categoryId: categoryId,
        ...data
      })
      .then(res=> console.log(res.status))
      .catch(err => console.log(err));
    }
    router.back();
  } catch (error) {
    console.error("Erro ao salvar produto:", error);
  }
};

// Excluir produto
const deleteProduct = async () => {
  if (!product.value?.id) return;

  const confirmation = window.confirm("Tem certeza que deseja excluir este produto?");
  if (!confirmation) return;

  try {
    await customFetch("DELETE", `${urls.products}${product.value.id}`);
    router.back();
  } catch (error) {
    console.error("Erro ao excluir produto:", error);
  }
};
</script>

<template>
  <h1 class="d-flex align-center">
    <v-btn variant="outlined" @click="$router.back()" icon="mdi-arrow-left" class="mr-4" size="small"></v-btn>
    Página Produto
  </h1>
  <br />

  <h3>Sobre o Produto</h3>
  <br />
  <v-card v-if="product" class="pa-4">
    <v-form @submit.prevent="saveProduct">
      <v-text-field v-model="product.name" label="Nome do Produto" required></v-text-field>
      <v-text-field v-model="product.description" label="Descrição do Produto"></v-text-field>
      <v-text-field v-model.number="product.price" label="Preço" type="number" step="0.01" required></v-text-field>
      <v-text-field v-model.number="product.quantity" label="Quantidade" type="number" required></v-text-field>

      <v-select
        v-if="id === 'new'"
        label="Categoria"
        :items="categoryNames"
        v-model="product.categoryId"
      ></v-select>

      <div class="mt-4 d-flex justify-space-between">
        <v-btn type="submit" color="primary">Salvar</v-btn>
        <v-btn v-if="id !== 'new'" @click="deleteProduct" color="red">Excluir</v-btn>
      </div>
    </v-form>
  </v-card>

  <v-alert v-else type="info" variant="outlined">Carregando produto...</v-alert>

  <br />
  <div v-if="id !== 'new'">
    <h3>Sobre a Categoria</h3>
    <br />
    <v-card v-if="category && !Array.isArray(category)" class="pa-4">
      <v-card-title>{{ category.name }}</v-card-title>
      <v-card-subtitle>{{ category.description }}</v-card-subtitle>
      <v-card-subtitle># {{ category.id }}</v-card-subtitle>
    </v-card>
    <v-alert v-else type="info" variant="outlined">Carregando categoria...</v-alert>
  </div>
</template>
