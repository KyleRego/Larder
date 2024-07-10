import { useState, useEffect } from "react";

import RecipesService from "./services/RecipesService";
import "./Recipes.css"

export default function Recipes()
{
    const [recipes, setRecipes] = useState([]);

    useEffect(() =>
    {
        const service = new RecipesService();

        service.getRecipes().then(result => {
            setRecipes(result);
        })
    }, []);

    return <>
        <h1>Recipes</h1>

        <RecipesTable recipes={recipes} />
    </>
}

function RecipesTable({recipes})
{
    let rows = recipes.map(recipe => RecipeRow(recipe));

    return <>
        <table class="recipesTable">
            <thead>
                <tr>
                    <th scope="col">
                        Name
                    </th>
                </tr>
            </thead>

            <tbody>
                {rows}
            </tbody>
        </table>
    </>
}

function RecipeRow(recipe)
{
    return <tr>
        <td>
            {recipe.name}
        </td>
    </tr>
}