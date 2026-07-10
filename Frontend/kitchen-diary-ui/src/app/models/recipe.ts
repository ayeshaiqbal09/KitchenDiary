export interface Recipe {
  id: number;
  title: string;
  summary: string;
  rating: number;
  notes: string;
  dateAdded: string;
  ingredients: any[];
  steps: any[];
  images: any[];
  tags: any[];
}