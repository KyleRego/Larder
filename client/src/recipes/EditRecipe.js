import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import RecipesService from "../services/RecipesService";
import recipeFormDataMapper from "./recipeFormDataMapper";

import RecipeForm from "./RecipeForm";

import "./EditRecipe.css";

export default function EditRecipe({units})
{
    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const recipeData = recipeFormDataMapper.map(formData);
        recipeData.recipeId = recipe.recipeId;

        const recipesService = new RecipesService();

        await recipesService.putRecipe(recipeData);
    }

    let { id } = useParams();
    const [recipe, setRecipe] = useState(null);

    useEffect(() =>
    {
        const recipesService = new RecipesService();

        recipesService.getRecipe(id).then(result => {
            setRecipe(result);
        });

    }, [id]);

    if (recipe === null || units === null) return <h1>Loading...</h1>;

    return (
        <>
            <h1>Editing recipe: {recipe.recipeName}</h1>

            <RecipeForm initialRecipe={recipe} units={units} handleSubmit={handleSubmit} />

            <Link to={`/recipes/${id}`}>Back to recipe</Link>
        </>
    );
}
