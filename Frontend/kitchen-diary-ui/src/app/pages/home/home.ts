import { Component, OnInit } from '@angular/core';
import { Recipe } from '../../models/recipe';
import { RecipeService } from '../../services/recipe';
import { JsonPipe } from '@angular/common';
import { RecipeCard } from '../../components/recipe-card/recipe-card';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [
    RecipeCard,
    RouterLink
],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

  recipes: Recipe[] = [];

  constructor(
  private recipeService: RecipeService) {}

  ngOnInit(): void {

    this.recipeService.getRecipes().subscribe({
    next: (recipes) => {
  console.log('SUCCESS');
  console.log(recipes);

  
  this.recipes = recipes;

},

    error: (err) => {
        console.error('Failed to load recipes', err);
     }
  });

}
}