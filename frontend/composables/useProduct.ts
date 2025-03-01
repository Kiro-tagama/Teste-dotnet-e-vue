import { watchEffect, ref, computed } from "vue";
import { useRoute, useRouter } from "vue-router";
import { urls, customFetch } from "~/composables/apiConfig";
import type { Products } from "~/interfaces/products";
import type { Categories } from "~/interfaces/categories";

export async function useProduct() {
  const route = useRoute();
  const router = useRouter();
  const { id } = route.params;

  const product = ref<Products | null>(null);
  const category = ref<Categories | Categories[] | null>(null);
  const categoryIsEmpty = ref<boolean>(false);

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
    }else{
      try {
        category.value = await $fetch<Categories>(`${urls.categories}${product.value?.categoryId}`);
      } catch (err: any) {
        if (err.response?.status === 404) {
          categoryIsEmpty.value = true;
          category.value = null; 
        }
      }
    }
  });

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
      categoryId: product.value.categoryId,
    };

    const getCategoryId =  Array.isArray(category.value) && 
      category.value?.find((cat) => cat.name === product.value?.categoryId)?.id;
  
    try {
      if (id !== "new") {
        await customFetch("PUT", `${urls.products}${id}`, {
          id: product.value.id,
          ...data
        });
      } else {
        await customFetch("POST", urls.products, {
          ...data,
          categoryId: getCategoryId,
        });
      }
      router.back();
    } catch (error) {
      console.error("Erro ao salvar produto:", error);
    }
  };
  

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

  return {
    id,
    product,
    category,
    categoryNames,
    categoryIsEmpty,
    saveProduct,
    deleteProduct
  }
}