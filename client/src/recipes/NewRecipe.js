import { Link } from "react-router-dom";

import RecipeForm from "./RecipeForm";

import RecipesService from "../services/RecipesService";
import recipeFormDataMapper from "./recipeFormDataMapper";

import "./NewRecipe.css";

export default function NewRecipe({units})
{
    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const recipeData = recipeFormDataMapper.map(formData);

        const recipesService = new RecipesService();

        await recipesService.postRecipe(recipeData);
    }

    return (<>
        <h1>New recipe:</h1>

        <RecipeForm initialRecipe={{ingredients: []}} units={units} handleSubmit={handleSubmit} />

        <Link to={"/recipes"}>Back to recipes</Link>
    </>);
}