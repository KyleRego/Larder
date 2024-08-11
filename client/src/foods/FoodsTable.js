import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";
import EditableQuantityTableCell from "../components/EditableQuantityTableCell";
import FoodsService from "../services/FoodsService";

export default function FoodsTable({foods, sortOrder, setSortOrder, units})
{
    let rows = foods.map(food => FoodRow(food, units));

    return <>
        <table className="foodsTable">
            <caption>Servings of ready to eat food.</caption>
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

function FoodRow(food, units)
{

    async function handleSubmitQuantity(e)
    {
        e.preventDefault();

        const quantityDto = {
            id: food.id
        };

        const formData = new FormData(e.target);

        quantityDto.amount = formData.get("amount");
        quantityDto.unitId = formData.get("unitId");

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

            <EditableQuantityTableCell quantity={food.quantity}
                                        handleSubmit={handleSubmitQuantity}
                                        units={units} />
        </tr>
    );
}