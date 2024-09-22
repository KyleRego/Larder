import { useState, useEffect } from "react";
import { v4 as uuidv4 } from 'uuid';
import { MdDone } from "react-icons/md";
import { MdCancel } from "react-icons/md";

import QuantityInput from "../components/QuantityInput";
import IngredientsService from "../services/IngredientsService";

export default function RecipeFormIngredientFormInputs({ingredient, units, setEditing, formRecipe, setFormRecipe})
{
    const [fetchedIngredients, setFetchedIngredients] = useState([]);
    const [fetchIngredientName, setFetchIngredientName] = useState(null);

    const thisComponentDomId = uuidv4().trim();

    useEffect(() =>
    {
        if (fetchIngredientName === null) return;

        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients(null, fetchIngredientName).then(result => {
            setFetchedIngredients(result);
        })
    }, [fetchIngredientName]);

    const fetchedIngredientItems = (fetchedIngredients.length !== 0) ? <ul className="ingredientOptionsList">
        {fetchedIngredients.map(ing => {
            return <li key={ing.id} >
                {ing.name}
            </li>;
        })}
    </ul> : "";

    function handleNameInput(e)
    {
        let newName = e.target.value;
        setFetchIngredientName(newName);
        console.log("handleNameInput fired");
    }

    function handleDoneEditing()
    {
        let newFormRecipe = structuredClone(formRecipe);
        for (let i = 0; i < newFormRecipe.ingredients.length; i += 1) {
            if (newFormRecipe.ingredients[i].id === ingredient.id) {
                const parent = document.getElementById(thisComponentDomId);
                const newIngredient = {
                    id: ingredient.id,
                    quantity: {}
                };

                newIngredient.name = parent.querySelector("input[name=\"name\"]").value
                newIngredient.quantity.amount = parent.querySelector("input[name=\"amount\"]")?.value;
                newIngredient.quantity.unitId = parent.querySelector("select[name=\"unitId\"]")?.value;

                newFormRecipe.ingredients[i] = newIngredient;
            }
        }
        setFormRecipe(newFormRecipe);
        setEditing(false);
    }

    function handleCancelEditing()
    {
        setEditing(false);
    }

    return <div id={thisComponentDomId} className="d-flex flex-wrap align-items-center column-gap-3">
        <div className="d-flex align-items-center column-gap-1">
            <label htmlFor="name">Name:</label>
            <input type="text" name="name" onInput={handleNameInput} defaultValue={ingredient.name} />
            {fetchedIngredientItems}
        </div>

        <div className="d-flex align-items-center column-gap-1">
            <QuantityInput quantity={ingredient.quantity} units={units} />
        </div>
        
        <MdDone onClick={handleDoneEditing} role="button"
                                    title="Done editing recipe ingredient"
                                    className="w-5 h-5" />
        
        <MdCancel onClick={handleCancelEditing} role="button" title="Cancel changing recipe ingredient" className="w-5 h-5" />
    </div>;
}
