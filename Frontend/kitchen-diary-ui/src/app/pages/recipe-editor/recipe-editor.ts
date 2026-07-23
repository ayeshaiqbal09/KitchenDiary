import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecipeService } from '../../services/recipe';
import { Recipe } from '../../models/recipe';
import { FormBuilder, ReactiveFormsModule, Validators, FormArray} from '@angular/forms';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { ToastService } from '../../services/toast.service';
import { ConfirmDialogService } from '../../services/confirm-dialog.service';


@Component({
  selector: 'app-recipe-editor',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './recipe-editor.html',
  styleUrl: './recipe-editor.css',
})
export class RecipeEditor implements OnInit {

  recipe?: Recipe;
  isCreateMode = false;
  isEditMode = false;
   selectedImage: string | null = null;
  recipeForm!: ReturnType<FormBuilder['group']>;
  styles = [

'Cake',

'Pizza',

'Italian',

'Chinese',

'Asian',

'Curry',

'Drinks',

'Cupcakes',

'Roasts',

'Mughlai',

'Indian',

'Thai',

'South Indian',

'North Indian',

'Biryani',

'Fast Food',

'Snacks',

'Seafood',

'Desserts'

];
  stars = [1, 2, 3, 4, 5];
  hoverRating = 0;
  constructor(
  private route: ActivatedRoute,
  private recipeService: RecipeService,
  private fb: FormBuilder,
  private router: Router,
   private toastService: ToastService,
   private confirmDialogService: ConfirmDialogService

) {
  this.recipeForm = this.fb.group({

  title: ['', Validators.required],

  summary: [''],

  rating:  [5],
  
    
  notes: [''],
  ingredients: this.fb.array([]),
  steps: this.fb.array([]),

  tags: this.fb.array([]),

  recipeStyle:['Cake']

});

}
get steps(): FormArray {

    return this.recipeForm.get('steps') as FormArray;

}
addStep(): void {

    this.steps.push(
        this.fb.control('')
    );

}
removeStep(index: number): void {

    this.steps.removeAt(index);

}
get ingredients(): FormArray {

  return this.recipeForm.get('ingredients') as FormArray;

}
addIngredient(): void {

  this.ingredients.push(

    this.fb.group({

      name: [''],

      quantity: ['']

    })

  );

}
removeIngredient(index: number): void {

  this.ingredients.removeAt(index);

}
get tags(): FormArray {

    return this.recipeForm.get('tags') as FormArray;

}
addTag(): void {

    this.tags.push(
        this.fb.control('')
    );

}
removeTag(index:number): void {

    this.tags.removeAt(index);

}
  ngOnInit(): void {

  const id = this.route.snapshot.paramMap.get('id');

  if (!id) {

    this.isCreateMode = true;
    this.isEditMode = true;
    
    return;
  }

  this.recipeService.getRecipeById(Number(id)).subscribe({
    next: (recipe) => {

      this.recipe = recipe;
       console.log(this.recipe);
    console.log(this.recipe.ingredients);
      this.recipeForm.patchValue({

      title: recipe.title,
      summary: recipe.summary,
      rating: recipe.rating,
      notes: recipe.notes,
      recipeStyle: recipe.recipeStyle

    });
    this.ingredients.clear();

recipe.ingredients.forEach(ingredient => {

    this.ingredients.push(

        this.fb.group({

            name: [ingredient.name],

            quantity: [ingredient.quantity]

        })

    );

});
recipe.steps.forEach(step => {

    this.steps.push(

        this.fb.control(step.instruction)

    );

});
recipe.tags.forEach(tag => {

    this.tags.push(

        this.fb.control(tag.name)

    );

});
    this.isEditMode = false;
    },
    error: (err) => {

  console.error(err);

  this.toastService.error('Something went wrong. Please try again.');

}
  });

  


  
}
enableEdit(): void {

  this.isEditMode = true;

}


onSubmit(): void {
  

  if (this.recipeForm.invalid) {
    this.recipeForm.markAllAsTouched();
    return;
  }

  const recipe = this.buildRecipe();
console.log("Recipe being sent:", recipe);
  if (this.isCreateMode) {
    
    this.createRecipe(recipe);
  } else {
    this.updateRecipe(recipe);
  }

}
private buildRecipe(): Recipe {

  return {
    id: this.recipe?.id ?? 0,
    title: this.recipeForm.value.title!,
    summary: this.recipeForm.value.summary!,
    rating: this.recipeForm.value.rating!,
    notes: this.recipeForm.value.notes ?? '',
    recipeStyle: this.recipeForm.value.recipeStyle!,
    dateAdded: this.recipe?.dateAdded ?? '',
    ingredients: this.ingredients.value.map((i: any) => ({
    name: i.name,
    quantity: i.quantity
})),
    steps: this.steps.value,
    images: this.recipe?.images ?? [],
    tags: this.tags.value,
    
    
  };

}
private updateRecipe(recipe: Recipe): void {

  this.recipeService.updateRecipe(recipe.id, recipe).subscribe({

    next: (updatedRecipe) => {

      this.recipe = updatedRecipe;
      this.isEditMode = false;
      this.toastService.success('Recipe updated successfully!');
      

    },

    error: (err) => {
      this.toastService.error('Something went wrong. Please try again.');
  console.log(err.error);

  console.log(JSON.stringify(err.error.errors, null, 2));
}

  });

}
private createRecipe(recipe: Recipe): void {

  this.recipeService.createRecipe(recipe).subscribe({

    next: (createdRecipe) => {

      this.toastService.success('Recipe created successfully!');
      
      this.router.navigate(['/recipes', createdRecipe.id]);

    },

    error: (err) => {
      this.toastService.error('Something went wrong. Please try again.');
  console.error('Create failed');
  console.error(err);
}

  });

}
async deleteRecipe(): Promise<void> {

  if (!this.recipe) {
    return;
  }

  const confirmed = await this.confirmDialogService.open({

  title: 'Delete Recipe',

  message: `Are you sure you want to delete "${this.recipe.title}"?`,

  confirmText: 'Delete',

  cancelText: 'Cancel'

});

if (!confirmed) {
  return;
}

  this.recipeService.deleteRecipe(this.recipe.id).subscribe({

    next: () => {

      this.toastService.success('Recipe deleted successfully!');

      this.router.navigate(['/']);

    },

    error: (err) => {

  console.error(err);

  this.toastService.error('Something went wrong. Please try again.');

}

  });
  

}
onImagesSelected(event: Event): void {

  if (!this.recipe) {
    return;
}

const recipeId = this.recipe.id;

  const input = event.target as HTMLInputElement;

  if (!input.files?.length) {
    return;
  }

  Array.from(input.files).forEach(file => {

    this.recipeService
      .uploadRecipeImage(recipeId, file)
      .subscribe({

        next: (image) => {
 console.log("Returned image:", image);
    if (!this.recipe) {
        return;
    }

    this.recipe = {
        ...this.recipe,
        images: [...this.recipe.images, image]
        
    };
    console.log("Images after upload:", this.recipe.images);

},

        error: (err) => {

  console.error(err);

  this.toastService.error('Something went wrong. Please try again.');

}

      });

  });

}
deleteImage(imageId: number): void {

  this.recipeService
      .deleteRecipeImage(imageId)
      .subscribe({

          next: () => {

    if (!this.recipe) {
        return;
    }

    this.recipe = {
        ...this.recipe,
        images: this.recipe.images.filter(i => i.id !== imageId)
    };

},

          error: (err) => {

  console.error(err);

  this.toastService.error('Something went wrong. Please try again.');

}

      });

}
openImage(imagePath: string): void {

  this.selectedImage = imagePath;

}
closeImage(): void {

  this.selectedImage = null;

}
setCoverImage(imageId: number): void {

  if (!this.recipe) {
    return;
  }

  this.recipeService
      .setCoverImage(this.recipe.id, imageId)
      .subscribe({

        next: () => {

          if (!this.recipe) {
            return;
          }

          this.recipe.images =
            this.recipe.images.map(image => ({

              ...image,

              isCoverImage: image.id === imageId

            }));

        },

        error: (err) => {

  console.error(err);

  this.toastService.error('Something went wrong. Please try again.');

}

      });

}
removeCoverImage() {

  this.recipeService
      .removeCoverImage(this.recipe!.id)
      .subscribe(() => {

        this.recipe!.images.forEach(i => {

          i.isCoverImage = false;

        });

      });

}
getThemeImage(): string {

   

        return 'page-themes/default.png';

    

   
}
}