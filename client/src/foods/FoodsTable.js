import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";

export default function FoodsTable({foods, sortOrder, setSortOrder})
{
    let rows = foods.map(food => FoodRow(food));

    return <>
        <table className="foodsTable">
            <caption>Foods</caption>
            <thead>
                <tr>
                    <SortingTableHeader columnName="Name" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <th>
                        Servings
                    </th>
                    <th>
                        Calories
                    </th>
                </tr>
            </thead>

            <tbody>
                {rows}
            </tbody>
        </table>
    </>
}

function FoodRow(food)
{
    let links = <Link to={`/foods/${food.id}`}>Details</Link>;

    if (food.recipeId !== undefined)
    {
        const recipeLink = <Link className="ml-2" to={`/recipes/${food.recipeId}`}>Recipe</Link>

        links = <>{links} {recipeLink}</>
    }

    return (
        <tr key={food.id}>
            <th scope="col">
                {food.name}
            </th>
            <td>
                {food.servings}
            </td>
            <td>
                {food.calories}
            </td>
            <td className="text-center">
                {links}
            </td>
        </tr>
    );
}