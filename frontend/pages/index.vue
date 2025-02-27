<script setup lang="ts">
import { ref, computed } from "vue";
import type { Categories } from "~/interfaces/categories.ts";

const urlBase: string = "http://localhost:5054";
const { data: categories, refresh } = useFetch<Categories[]>(urlBase + "/api/categories");

const selectedCategory = ref<Partial<Categories> | null>(null);
const dialog = ref(false);
const isEditing = ref(false);

const openEditModal = (category: Categories, event: Event) => {
  event.stopPropagation();
  selectedCategory.value = { ...category };
  isEditing.value = true;
  dialog.value = true;
};

const openCreateModal = () => {
  selectedCategory.value = { name: "", description: "" }; // Criar sem ID e createdAt
  isEditing.value = false;
  dialog.value = true;
};

// Função para salvar (Criar ou Editar)
const saveCategory = async () => {
  if (!selectedCategory.value?.name) {
    alert("O nome da categoria é obrigatório!");
    return;
  }

  try {
    if (isEditing.value) {
      // Atualizar categoria
      await fetch(`${urlBase}/api/categories/${selectedCategory.value.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: selectedCategory.value.name,
          description: selectedCategory.value.description,
          updatedAt: new Date().toISOString()
        }),
      });
    } else {
      // Criar nova categoria
      await fetch(`${urlBase}/api/categories`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: selectedCategory.value.name,
          description: selectedCategory.value.description || null, // Se não preencher, envia null
          createdAt: new Date().toISOString(),
          updatedAt: new Date().toISOString()
        }),
      })
      .then((res) => alert(res.status))
      .catch((err) => alert("erro "+err.status))
    }

    refresh(); // Atualiza a lista de categorias
    dialog.value = false;
  } catch (error) {
    console.error("Erro ao salvar categoria:", error);
  }
};

// Input para busca de categorias
const inputCategory = ref("");

const newCategories = computed(() => 
  categories.value?.filter((c: Categories) => 
    !inputCategory.value || c.name.includes(inputCategory.value)
  ) || []
);
const categoryNames = computed(() => categories.value?.map(i => i.name) || []);
</script>

<template>
  <h1>Categorias</h1>
  <br>
  <div class="d-flex flex-row" style="gap:.5rem">
    <v-autocomplete
      label="Buscar"
      v-model="inputCategory"
      :items="categoryNames"
    ></v-autocomplete>
    <v-btn height="56" @click="openCreateModal">ADD</v-btn>
  </div>

  <div class="categories-grid">
    <CardCategories key="0" title="ALL" to="/listProducts/all" />
    <CardCategories
      v-for="category in newCategories"
      :key="category.id"
      :title="category.name"
      :to="`/listProducts/${category.id}`"
      :openModal="(event) => openEditModal(category, event)"
    />
  </div>

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
