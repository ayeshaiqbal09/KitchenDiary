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
  stars = [1, 2, 3, 4, 5];
  constructor(private router: Router) {}

  viewRecipe(): void {
    this.router.navigate(['/recipes', this.recipe.id]);
  }
  getCoverImage(): string {

  const cover =
    this.recipe.images.find(i => i.isCoverImage);

  if (cover) {

    return 'http://localhost:5281/' + cover.imagePath;

  }

  

  return this.getStyleImage(this.recipe.recipeStyle);

}
getStyleImage(style?: string): string {

  if (!style || style.trim() === '') {

    return 'logo/kitchen-diary-logo.webp.png';

  }

  return `recipe-styles/${
    style
      .toLowerCase()
      .replace(/\s+/g, '-')
  }.webp.png`;

}
}