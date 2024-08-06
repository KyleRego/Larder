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
                    <SortingTableHeader columnName="Quantity" sortOrder={sortOrder} setSortOrder={setSortOrder} />
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
    if (food.recipeId !== null)
    {
        const recipeLink = <Link className="ml-2" to={`/recipes/${food.recipeId}`}>Recipe</Link>
    }

    async function handleSubmitQuantity(e)
    {
        e.preventDefault();

        const newQuantity = (new FormData(e.target)).get("quantity");

        const quantityDto = {
            id: food.id,
            amount: newQuantity
        };

        const service = new FoodsService();

        await service.patchFood(quantityDto);
    }

    return (
        <tr key={food.id}>
            <th scope="row">
                <Link to={`/foods/${food.id}`}>
                    {food.name}
                </Link>
            </th>

            <EditableQuantityTableCell quantity={food.quantity?.amount} handleSubmit={handleSubmitQuantity} />
        </tr>
    );
}