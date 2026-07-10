import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { RecipeDetails } from './pages/recipe-details/recipe-details';

export const routes: Routes = [
  {
    path: '',
    component: Home
  },
  {
    path: 'recipes/:id',
    component: RecipeDetails
  }
];