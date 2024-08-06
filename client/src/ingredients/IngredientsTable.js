import { Link } from "react-router-dom";

import EditableQuantityTableCell from "../components/EditableQuantityTableCell";
import SortingTableHeader from "../components/SortingTableHeader";

import "./IngredientsTable.css";
import IngredientsService from "../services/IngredientsService";

export default function IngredientsTable({ingredients, sortOrder, setSortOrder})
{
    const ingredientRows = ingredients.map(ingredient => <IngredientRow key={ingredient.id} ingredient={ingredient} />);

    return (
        <>
            <table className="ingredientsTable">
                <caption>
                    Ingredients on hand
                </caption>
                <thead>
                    <tr>
                        <SortingTableHeader columnName="Name" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                        <SortingTableHeader columnName="Quantity" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    </tr>
                </thead>
                <tbody>
                    {ingredientRows}
                </tbody>
            </table>
        </>
    )
}

function IngredientRow({ingredient})
{
    async function handleSubmitQuantity(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);
        const newAmount = formData.get("quantity");

        const dto = {
            id: ingredient.id,
            amount: newAmount
        };

        const ingredientsService = new IngredientsService();

        await ingredientsService.patchQuantity(dto);
    }

    return <tr>
        <th scope="row">
            <Link to={`/ingredients/${ingredient.id}`}>
                {ingredient.name}
            </Link>
        </th>

        <EditableQuantityTableCell quantity={ingredient.quantity?.amount} handleSubmit={handleSubmitQuantity} />
    </tr>
}
