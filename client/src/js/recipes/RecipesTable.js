import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";

export default function RecipesTable({recipes, sortOrder, setSortOrder}) {
    const rows = recipes.map(recipe => RecipeRow(recipe));

    return <>
        <table>
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
        <tr key={recipe.id}>
            <th scope="row">
                <Link to={`/recipes/${recipe.id}`}>
                    {recipe.name}
                </Link>
            </th>
        </tr>
    );
}
