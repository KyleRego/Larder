import { Link } from "react-router-dom";

import EditableQuantityTableCell from "../components/EditableQuantityTableCell";
import SortingTableHeader from "../components/SortingTableHeader";

import "./IngredientsTable.css";
import IngredientsService from "../services/IngredientsService";

export default function IngredientsTable({ingredients, sortOrder, setSortOrder, units})
{
    const ingredientRows = ingredients.map(ingredient => <IngredientRow key={ingredient.id}
                                                                        ingredient={ingredient}
                                                                        units={units} />);

    return (
        <>
            <table className="ingredientsTable">
                <caption>
                    Ingredients reading to be used for cooking
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

function IngredientRow({ingredient, units})
{
    async function handleSubmitQuantity(e)
    {
        e.preventDefault();

        const dto = {
            id: ingredient.id,
        };

        const formData = new FormData(e.target);
        dto.amount = formData.get("amount");
        dto.unitId = formData.get("unitId");

        const ingredientsService = new IngredientsService();

        ingredientsService.patchQuantity(dto).then((result) => {
            console.log(result);
        });
    }

    return <tr>
        <th scope="row">
            <Link to={`/ingredients/${ingredient.id}`}>
                {ingredient.name}
            </Link>
        </th>

        <EditableQuantityTableCell quantity={ingredient.quantity}
                                    units={units}
                                    handleSubmit={handleSubmitQuantity} />
    </tr>
}
