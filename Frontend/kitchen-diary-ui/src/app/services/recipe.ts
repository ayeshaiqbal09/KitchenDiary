import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recipe, RecipeImage } from '../models/recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private apiUrl = 'http://localhost:5281/api/recipes';

  constructor(private http: HttpClient) {}

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.apiUrl);
  }
  getRecipeById(id: number): Observable<Recipe> {
  return this.http.get<Recipe>(`${this.apiUrl}/${id}`);
  }
  updateRecipe(id: number, recipe: Recipe): Observable<Recipe> {
    return this.http.put<Recipe>(
        `${this.apiUrl}/${id}`,
        recipe
    );
}
  createRecipe(recipe: Recipe): Observable<Recipe> {
    return this.http.post<Recipe>(this.apiUrl, recipe);
  }
  deleteRecipe(id: number): Observable<void> {
  return this.http.delete<void>(`${this.apiUrl}/${id}`);
}
uploadRecipeImage(recipeId: number, file: File) {

  const formData = new FormData();

  formData.append("Image", file);

  return this.http.post<RecipeImage>(
    `${this.apiUrl}/${recipeId}/images`,
    formData
  );

}
deleteRecipeImage(imageId: number) {

  return this.http.delete(
    `${this.apiUrl}/images/${imageId}`
  );

}
setCoverImage(recipeId: number, imageId: number) {

  return this.http.put(
    `${this.apiUrl}/${recipeId}/cover/${imageId}`,
    {}
  );

}
}