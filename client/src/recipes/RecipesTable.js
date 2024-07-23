import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";

export default function RecipesTable({recipes, sortOrder, setSortOrder})
{
    let rows = recipes.map(recipe => RecipeRow(recipe));

    return <>
        <table className="recipesTable">
            <caption>Recipes</caption>
            <thead>
                <tr>
                    <SortingTableHeader columnName="Name" sortOrder={sortOrder} setSortOrder={setSortOrder} />
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
        <tr key={recipe.recipeId}>
            <th scope="col">
                {recipe.recipeName}
            </th>
            <td className="text-center">
                <Link to={`/recipes/${recipe.recipeId}`}>
                    Details
                </Link>
            </td>
        </tr>
    );
}