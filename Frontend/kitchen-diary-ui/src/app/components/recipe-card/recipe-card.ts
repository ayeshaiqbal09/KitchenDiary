import { Component, Input } from '@angular/core';
import {Recipe} from '../../models/recipe'

@Component({
  selector: 'app-recipe-card',
  imports: [],
  templateUrl: './recipe-card.html',
  styleUrl: './recipe-card.css',
})
export class RecipeCard {

  @Input()
  recipe!:Recipe;
}
