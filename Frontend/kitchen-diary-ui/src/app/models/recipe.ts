export interface Recipe
{
    id : number;

    title: string;

    rating: number;

    recipeType: string;
    
    coverImage?: string;
}