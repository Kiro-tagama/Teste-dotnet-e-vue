<script setup lang="ts">
  const {
    id,
    product,
    category,
    categoryNames,
    saveProduct,
    deleteProduct
  } = await useProduct();
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
        <v-btn v-if="id !== 'new'" @click="deleteProduct" color="red">Excluir</v-btn>
        <v-btn type="submit" color="primary">Salvar</v-btn>
      </div>
    </v-form>
  </v-card>
  <v-alert v-else type="info" variant="outlined">Carregando produto...</v-alert>

  <br />
  <div v-if="id !== 'new'">
    <h3>Sobre a Categoria</h3>
    <br />
    <v-card v-if="category" class="pa-4">
      <v-card-title>{{ category.name }}</v-card-title>
      <v-card-subtitle>{{ category.description }}</v-card-subtitle>
      <v-card-subtitle># {{ category.id }}</v-card-subtitle>
    </v-card>
    <v-alert v-else type="info" variant="outlined">Carregando categoria...</v-alert>
  </div>
</template>
