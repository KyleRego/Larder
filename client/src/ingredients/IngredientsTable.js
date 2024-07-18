import { useState } from "react";
import { Link } from "react-router-dom";
import { TfiExchangeVertical } from "react-icons/tfi";
import { MdDone } from "react-icons/md";

import "./IngredientsTable.css";
import IngredientsService from "../services/IngredientsService";

export default function IngredientsTable({ingredients})
{
    const ingredientRows = ingredients.map(ingredient => <IngredientRow key={ingredient.id} ingredientProp={ingredient} />);

    return (
        <>
            <table>
                <caption>
                    Ingredients on hand
                </caption>
                <thead>
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Quantity</th>
                    </tr>
                </thead>
                <tbody>
                    {ingredientRows}
                </tbody>
            </table>
        </>
    )
}

function IngredientRow({ingredientProp})
{
    const [editing, setEditing] = useState(false);
    const [ingredient, setIngredient] = useState(ingredientProp);

    function editQuantity()
    {
        setEditing(true);
    }

    async function updateQuantity(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const ingredientData = {
            id: ingredient.id,
            quantity: formData.get("quantity")
        };

        const ingredientsService = new IngredientsService();

        const updatedIngredient = await ingredientsService.patchQuantity(ingredientData);
        setIngredient(updatedIngredient);
        setEditing(false);
    }

    let amountCell = <IngredientAmountCell editing={editing}
                                            ingredient={ingredient}
                                            editQuantity={editQuantity}
                                            updateQuantity={updateQuantity} />

    return <tr>
        <th scope="row">
            <span className="mr-2">{ingredient.name}</span>
            <Link to={`/ingredients/${ingredient.id}`}>Details</Link>
        </th>

        {amountCell}
    </tr>
}

function IngredientAmountCell({editing, ingredient, editQuantity, updateQuantity})
{
    if (editing === false)
    {
        return <td>
            <span className="mr-2">{ingredient.quantity}</span>
            <span className="mr-2">{ingredient.unit}</span>
            <TfiExchangeVertical onClick={editQuantity} />
        </td>
    }
    else
    {
        return <td>
            <form onSubmit={updateQuantity}>
                <input name="quantity" type="number" defaultValue={ingredient.quantity}></input>
                <button type="submit">
                    <MdDone />
                </button>
            </form>
        </td>
    }
}