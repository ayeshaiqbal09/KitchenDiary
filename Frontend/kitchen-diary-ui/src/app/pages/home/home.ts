import { Component, OnInit } from '@angular/core';
import { Recipe } from '../../models/recipe';
import { RecipeService } from '../../services/recipe';
import { JsonPipe } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-home',
  imports: [JsonPipe],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

  recipes: Recipe[] = [];

  constructor(
  private recipeService: RecipeService,
  private cdr: ChangeDetectorRef
) {}

  ngOnInit(): void {

  console.log('Home loaded');

  this.recipeService.getRecipes().subscribe({
    next: (recipes) => {
  console.log('SUCCESS');
  console.log(recipes);

  this.recipes = recipes;

  console.log('Length after assignment:', this.recipes.length);

  this.cdr.detectChanges();
},

    error: (err) => {
      console.log('FAILED');
      console.error(err);
    }
  });

}
}