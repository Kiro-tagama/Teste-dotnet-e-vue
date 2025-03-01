import type { Categories } from '~/interfaces/categories.ts';
import { ref, computed } from "vue";
import { customFetch, urls } from './apiConfig';

export function useHome() {
  const { data: categories, refresh } = useFetch<Categories[]>(urls.categories);

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

    const data = {
      name: selectedCategory.value.name,
      description: selectedCategory.value.description || "",
      updatedAt: new Date().toISOString()
    }

    try {
      if (isEditing.value) {
        // Atualizar categoria
        const id = selectedCategory.value.id
        await customFetch("PUT", urls.categories+id,{
          id:selectedCategory.value.id,
          ...data
        })
        .then(res => console.log(res))
        .catch((err) => alert("erro " + err.message))
      } else {
        // Criar nova categoria
        await customFetch("POST",urls.categories,data)
        .then(res => console.log(res.status))
        .catch((err) => alert("erro " + err.status))
      }

      refresh(); 
      dialog.value = false;
    } catch (error) {
      console.error("Erro ao salvar categoria:", error);
    }
  };

  const deleteCategory = async () => {
    if (!selectedCategory.value?.id) {
      alert("Selecione uma categoria para excluir!");
      return;
    }

    const confirmation = window.confirm("Tem certeza que deseja excluir esta categoria?");
    if (!confirmation) {
      dialog.value = false;
      return;
    };

    try {
      await customFetch("DELETE", urls.categories + selectedCategory.value.id)
      .then(res=> console.log(res.status))
      .catch(err => console.log(err));

      refresh();
      dialog.value = false;
    } catch (error) {
      console.error("Erro ao excluir categoria:", error);
    }
  }

  // Input para busca de categorias
  const inputCategory = ref("");

  const newCategories = computed(() =>
    categories.value?.filter((c: Categories) =>
      !inputCategory.value || c.name.includes(inputCategory.value)
    ) || []
  );

  console.log(newCategories)

  const categoryNames = computed(() => categories.value?.map(i => i.name) || []);

  return {
    newCategories,
    inputCategory,
    categoryNames,
    openCreateModal,
    openEditModal,
    selectedCategory,
    dialog,
    isEditing,
    saveCategory,
    deleteCategory
  }
}
