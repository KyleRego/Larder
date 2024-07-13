import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

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

        <Link to="/recipes/new">New recipe</Link>
    </>
}

function RecipesTable({recipes})
{
    let rows = recipes.map(recipe => RecipeRow(recipe));

    return <>
        <table className="recipesTable">
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
    return (
        <tr key={recipe.id}>
            <td>
                {recipe.name}
            </td>
            <td className="text-center">
                <Link to={`/recipes/${recipe.id}`}>
                    Details
                </Link>
            </td>
        </tr>
    );
}