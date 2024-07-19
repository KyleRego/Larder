import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import RecipesService from "../services/RecipesService";
import UnitsService from "../services/UnitsService";

import RecipeForm from "./RecipeForm";

import "./EditRecipe.css";

export default function EditRecipe()
{
    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const recipeData = {recipeId: recipe.recipeId};
        const ingredientsData = [];

        formData.entries().forEach(entry => {
            const key = entry[0];
            const val = entry[1];

            if (key === "recipeName")
            {
                recipeData["recipeName"] = val;
            }
            else if (key.startsWith("ingredient"))
            {
                const index = key.substring(key.length - 1);

                if (!ingredientsData[index])
                {
                    ingredientsData[index] = {};
                }

                const restOfKey = key.substring("ingredient".length)

                if (restOfKey.startsWith("Name"))
                {
                    ingredientsData[index]["ingredientName"] = val;
                }
                else if (restOfKey.startsWith("Amount"))
                {
                    ingredientsData[index]["amount"] = val;
                }
                else if (restOfKey.startsWith("Unit"))
                {
                    ingredientsData[index]["unitId"] = val;
                }
            }
        });

        recipeData["ingredients"] = ingredientsData;

        // TODO: Use DI for this?
        const recipesService = new RecipesService();

        await recipesService.putRecipe(recipeData);
    }

    let { id } = useParams();
    const [recipe, setRecipe] = useState(null);
    const [units, setUnits] = useState(null);

    useEffect(() =>
    {
        const recipesService = new RecipesService();

        recipesService.getRecipe(id).then(result => {
            setRecipe(result);
        });

        // TODO: Units data can be pulled way up in the state of the app
        const unitsService = new UnitsService();

        unitsService.getUnits().then(result => {
            setUnits(result);
        })
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
