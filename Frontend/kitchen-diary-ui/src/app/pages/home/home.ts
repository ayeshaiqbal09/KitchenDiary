import { Component, OnInit } from '@angular/core';
import { Recipe } from '../../models/recipe';
import { RecipeService } from '../../services/recipe';
import { JsonPipe } from '@angular/common';
import { RecipeCard } from '../../components/recipe-card/recipe-card';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [
    RecipeCard,
    FormsModule,

    RouterLink
],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

  recipes: Recipe[] = [];

searchTerm = '';

  constructor(
  private recipeService: RecipeService) {}

  ngOnInit(): void {

    this.recipeService.getRecipes().subscribe({
    next: (recipes) => {
  console.log('SUCCESS');
  console.log(recipes);

  this.loadRecipes();
  this.recipes = recipes;

},

    error: (err) => {
        console.error('Failed to load recipes', err);
     }
  });

}
private loadRecipes(): void {

  this.recipeService.getRecipes().subscribe({

    next: (recipes) => {

      this.recipes = recipes;

    },

    error: (err) => {

      console.error('Failed to load recipes', err);

    }

  });

}
search(): void {

  if (!this.searchTerm.trim()) {

    this.loadRecipes();

    return;

  }

  this.recipeService
      .searchRecipes(this.searchTerm)
      .subscribe({

        next: recipes => {

          this.recipes = recipes;

        },

        error: err => console.error(err)

      });

}
}