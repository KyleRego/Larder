import { Link } from "react-router-dom";

import EditableQuantityTableCell from "../components/EditableQuantityTableCell";
import SortingTableHeader from "../components/SortingTableHeader";

import "./IngredientsTable.css";
import IngredientsService from "../services/IngredientsService";

export default function IngredientsTable({ingredients, setIngredients, sortOrder, setSortOrder, units})
{
    const ingredientRows = ingredients.map(ingredient => <IngredientRow key={ingredient.id}
                                                                        ingredient={ingredient}
                                                                        ingredients={ingredients}
                                                                        setIngredients={setIngredients}
                                                                        units={units} />);

    return <table className="ingredientsTable">
                <caption>
                    Ingredients for cooking
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
            </table>;
}

function IngredientRow({ingredient, ingredients, setIngredients, units})
{
    async function handleSubmitQuantity(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const dto = {
            id: ingredient.id,
            unitId: formData.get("unitId"),
            amount: formData.get("amount")
        };

        const ingredientsService = new IngredientsService();

        ingredientsService.patchQuantity(dto).then((result) => {
            // TODO: There must be a better idiom in JavaScript for this
            let newIngredients = structuredClone(ingredients);
            for (let i = 0; i < newIngredients.length; i += 1) {
                if (newIngredients[i].id === ingredient.id) {
                    newIngredients[i] = result;
                }
            }
            setIngredients(newIngredients);
        });
    }

    return <tr>
        <th scope="row">
            <Link to={`/ingredients/${ingredient.id}`}>
                {ingredient.name}
            </Link>
        </th>

        <EditableQuantityTableCell quantity={ingredient.quantity}
                                    handleSubmit={handleSubmitQuantity} />
    </tr>;
}
