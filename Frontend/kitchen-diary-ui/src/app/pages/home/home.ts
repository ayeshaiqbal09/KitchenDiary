import { Component } from '@angular/core';
import { RecipeCard } from '../../components/recipe-card/recipe-card';
import {Recipe} from '../../models/recipe';
import{OnInit} from '@angular/core';
import { RecipeService } from '../../services/recipe.service';

@Component({
  selector: 'app-home',
  imports: [RecipeCard],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit{

  constructor(private recipeService: RecipeService) { }
  recipes: Recipe[]=[];

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe({
      next:(recipes)=>{
        console.log(recipes);
        this.recipes=recipes;
        
      },
      error: (error) =>{
        console.error(error);
      }
    });
  }
}
