export interface Recipe {
  id: number;
  title: string;
  summary: string;
  rating: number;
  notes: string;
  dateAdded: string;
  ingredients: Ingredient[];
steps: RecipeStep[];
  images: RecipeImage[];
}
export interface Ingredient {

  id: number;

  name: string;

  quantity: string | null;

}

export interface RecipeStep {

  id: number;

  stepNumber: number;

  instruction: string;

}

export interface RecipeImage {

  id: number;
  imagePath: string;

}

export interface Tag {

  id: number;
  name: string;

}

export interface Recipe {

  id: number;

  title: string;

  summary: string;

  rating: number;

  notes: string;

  dateAdded: string;

  ingredients: Ingredient[];

  steps: RecipeStep[];

  images: RecipeImage[];

  tags: Tag[];
  
  recipeStyle: string;

}
export interface RecipeImage {

  id: number;

  imagePath: string;

  isCoverImage: boolean;

}