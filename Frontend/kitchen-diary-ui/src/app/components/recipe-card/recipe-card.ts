import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Recipe } from '../../models/recipe';

@Component({
  selector: 'app-recipe-card',
  imports: [],
  templateUrl: './recipe-card.html',
  styleUrl: './recipe-card.css',
})
export class RecipeCard {

  @Input({ required: true })
  recipe!: Recipe;

  constructor(private router: Router) {}

  viewRecipe(): void {
    this.router.navigate(['/recipes', this.recipe.id]);
  }
  getCoverImage(): string {

  const cover = this.recipe.images.find(i => i.isCoverImage);

  return cover
      ? 'http://localhost:5281/' + cover.imagePath
      : '';
}
}