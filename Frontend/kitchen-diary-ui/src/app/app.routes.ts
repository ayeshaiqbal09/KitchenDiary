import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { RecipeEditor } from './pages/recipe-editor/recipe-editor';


export const routes: Routes = [
  {
    path: '',
    component: Home
  },
  {
    path: 'recipes/new',
    component: RecipeEditor
  },
  {
    path: 'recipes/:id',
    component: RecipeEditor
  }
];