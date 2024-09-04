import { useState, useEffect } from "react";

import { MdDone } from "react-icons/md";
import { MdCancel } from "react-icons/md";

import QuantityInput from "../components/QuantityInput";
import IngredientsService from "../services/IngredientsService";

export default function RecipeFormIngredientFormInputs({ingredient, units, setEditing, formRecipe, setFormRecipe})
{
    const [formIngredient, setFormIngredient] = useState(ingredient);
    const [fetchedIngredients, setFetchedIngredients] = useState([]);
    const [fetchIngredientName, setFetchIngredientName] = useState(null);

    useEffect(() =>
    {
        if (fetchIngredientName === null) return;

        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients(null, fetchIngredientName).then(result => {
            setFetchedIngredients(result);
        })
    }, [fetchIngredientName]);

    const quantity = ingredient?.quantity;

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

        const newFormIngredient = structuredClone(formIngredient);
        newFormIngredient.name = newName;
        setFormIngredient(newFormIngredient);
    }

    function handleDoneEditing()
    {
        let newFormRecipe = structuredClone(formRecipe);
        for (let i = 0; i < newFormRecipe.ingredients.length; i += 1) {
            if (newFormRecipe.ingredients[i].id === formIngredient.id) {
                newFormRecipe.ingredients[i] = formIngredient;
            }
        }
        setFormRecipe(newFormRecipe);
        setEditing(false);
    }

    function handleCancelEditing()
    {
        let newFormRecipe = structuredClone(formRecipe);
        for (let i = 0; i < newFormRecipe.ingredients.length; i += 1) {
            if (newFormRecipe.ingredients[i].id === formIngredient.id) {
                newFormRecipe.ingredients[i] = ingredient;
            }
        }
        setFormRecipe(newFormRecipe);
        setEditing(false);
    }

    return <div className="d-flex flex-wrap align-items-center column-gap-3">
        <div className="d-flex align-items-center column-gap-1">
            <label htmlFor="name">Name:</label>
            <input type="text" onInput={handleNameInput} defaultValue={ingredient.name} />
            {fetchedIngredientItems}
        </div>

        <div className="d-flex align-items-center column-gap-1">
            <QuantityInput quantity={{quantity}} units={units} />
        </div>

        <div>
            <MdDone onClick={handleDoneEditing} role="button" title="Done editing recipe ingredient" className="w-5 h-5" />
        </div>

        <div>
            <MdCancel onClick={handleCancelEditing} role="button" title="Cancel changing recipe ingredient" className="w-5 h-5" />
        </div>
    </div>;
}
