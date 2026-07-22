import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { RecipeEditor } from './pages/recipe-editor/recipe-editor';
import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { authGuard } from './guards/auth-guard';
import { About } from './pages/about/about';
import { Contact } from './pages/contact/contact';
import { NotFound } from './pages/not-found/not-found';

export const routes: Routes = [
  {
    path: '',
    component: Home,
    
    canActivate: [authGuard]
  },
  {
    path: 'recipes/new',
    component: RecipeEditor,
  canActivate: [authGuard]
  },
  {
    path: 'recipes/:id',
    component: RecipeEditor,
    canActivate: [authGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  }
  ,
  { path: 'register', component: RegisterComponent },

  {path: 'about',
  component: About},

  {
  path: 'contact',
  component: Contact
},
{
    path: '**',
    component: NotFound
},

  { path: '**', redirectTo: '' }
];