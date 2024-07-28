import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";
import EditableQuantityTableCell from "../components/EditableQuantityTableCell";
import FoodsService from "../services/FoodsService";

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
                        Quantity
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
    let links = <Link className="ml-2" to={`/foods/${food.id}`}>Details</Link>;

    if (food.recipeId !== null)
    {
        const recipeLink = <Link className="ml-2" to={`/recipes/${food.recipeId}`}>Recipe</Link>

        links = <>{links} {recipeLink}</>
    }

    async function handleSubmitQuantity(e)
    {
        e.preventDefault();

        const newQuantity = (new FormData(e.target)).get("quantity");

        const patchFood = {
            id: food.id,
            quantity: newQuantity
        };

        const service = new FoodsService();

        await service.patchFood(patchFood);
    }

    return (
        <tr key={food.id}>
            <th scope="col">
                {food.name}

                {links}
            </th>

            <EditableQuantityTableCell quantity={food.quantity} handleSubmit={handleSubmitQuantity} />
        </tr>
    );
}