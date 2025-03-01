<script setup lang="ts">
  const {
    newCategories,
    inputCategory,
    categoryNames,
    selectedCategory,
    dialog,
    isEditing,
    openCreateModal,
    openEditModal,
    saveCategory,
    deleteCategory
  } = useHome();
</script>

<template>
  <h1>Categorias</h1>
  <br>
  <div class="d-flex flex-row" style="gap:.5rem" >
    <v-autocomplete
      label="Buscar"
      v-model="inputCategory"
      :items="categoryNames"
    ></v-autocomplete>
    <v-btn height="56" @click="openCreateModal">ADD</v-btn>
  </div>

  <div class="categories-grid" v-if="newCategories.length > 0">
    <CardCategories key="0" title="ALL" to="/listProducts/all" />
    <CardCategories
      v-for="category in newCategories"
      :key="category.id"
      :title="category.name"
      :to="`/listProducts/${category.id}`"
      :openModal="(event) => openEditModal(category, event)"
    />
  </div>
  <div v-else>Carregando Categorias...</div>

  <!-- Modal de Criação/Edição -->
  <v-dialog v-model="dialog" max-width="400">
    <v-card>
      <v-card-title>{{ isEditing ? "Editar Categoria" : "Criar Categoria" }}</v-card-title>
      <v-card-text>
        <v-text-field label="Nome" v-model="selectedCategory!.name" required></v-text-field>
        <v-text-field label="Descrição" v-model="selectedCategory!.description"></v-text-field>
        <v-text-field v-if="isEditing" label="ID" v-model="selectedCategory!.id" disabled></v-text-field>
      </v-card-text>
      <v-card-actions>
        <v-btn v-if="isEditing" color="red" style="margin-right: auto;" @click="deleteCategory">Apagar</v-btn>
        <v-btn @click="dialog = false">Cancelar</v-btn>
        <v-btn color="primary" @click="saveCategory">{{ isEditing ? "Salvar" : "Criar" }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.categories-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 16px;
}
</style>
