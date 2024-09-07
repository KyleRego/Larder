import { Link } from "react-router-dom";

import RecipeForm from "./RecipeForm";

import RecipesService from "../services/RecipesService";

export default function NewRecipe({units})
{
    async function handleSubmit(e, formRecipe)
    {
        e.preventDefault();
        const recipeName = new FormData(e.target).get("recipeName");
        formRecipe.name = recipeName;

        const recipesService = new RecipesService();

        await recipesService.postRecipe(formRecipe);
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