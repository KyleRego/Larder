import { Link } from "react-router-dom";

import RecipeForm from "./RecipeForm";

import RecipesService from "../services/RecipesService";
import RecipeFormDataMapper from "./RecipeFormDataMapper";

export default function NewRecipe({units})
{
    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const recipeData = RecipeFormDataMapper.map(formData);

        const recipesService = new RecipesService();

        await recipesService.postRecipe(recipeData);
    }

    let initialRecipe = {
        name: "New recipe",
        ingredients: []
    };

    return (<>
        <h1>New recipe:</h1>

        <div className="card shadow-sm">
            <div className="card-body">
                <RecipeForm recipe={initialRecipe} units={units} handleSubmit={handleSubmit} />
            </div>
        </div>

        <Link to={"/recipes"}>Back to recipes</Link>
    </>);
}