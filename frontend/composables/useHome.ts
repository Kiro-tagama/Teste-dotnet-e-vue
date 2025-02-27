import type { Category } from '~/interfaces/categories.ts';
import { useFetch } from '#app';

export function useCategories() {
  const { data: categories, refresh } = useFetch<Category[]>('/api/categories');
  
  return { categories, refresh };
}
