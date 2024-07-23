import { useState, useEffect } from "react";
import { v4 as uuidv4 } from "uuid";

import UnitSelectOptions from "../units/UnitSelectOptions";

import IngredientsService from "../services/IngredientsService";

import "./RecipeForm.css";

export default function RecipeForm({initialRecipe, units, handleSubmit})
{
    const [recipe, setRecipe] = useState(initialRecipe);

    function removeIngredient(index)
    {
        const newIngredients = [];

        recipe.ingredients.forEach((ri, i) => {
            if (index !== i) { newIngredients.push(ri); }
        });

        const newRecipe = structuredClone(recipe);
        newRecipe.ingredients = newIngredients;

        setRecipe(newRecipe);
    }

    function addRecipeIngredient()
    {
        const newRecipe = structuredClone(recipe);
        newRecipe.ingredients.push({
            ingredientName: "",
            recipeIngredientId: uuidv4(),
            amount: 0,
            unitName: "",
            unitId: null
        });
        setRecipe(newRecipe);
    }

    let recipeIngredientFormItems = recipe.ingredients.map((recipeIngredient, i) => {

        return <RecipeIngredientFormItem    key={recipeIngredient.recipeIngredientId}
                                            initialIngredient={recipeIngredient}
                                            i={i}
                                            units={units}
                                            removeIngredient={removeIngredient} />
    });

    return (
        <>
            <form className="recipeForm" onSubmit={handleSubmit}>
                <div className="flex align-items-center">
                    <label htmlFor="recipeName"><strong>Recipe name:</strong></label>
                    <input className="flex-grow-1" id="recipeName" name="recipeName" type="text" defaultValue={recipe.recipeName} />
                </div>
    
                {recipeIngredientFormItems}

                <div>
                    <button type="button" onClick={addRecipeIngredient}>New ingredient</button>
                </div>

                <div>
                    <button type="submit">Submit</button>
                </div>
            </form>
        </>
    );
}

function RecipeIngredientFormItem({initialIngredient, i, units, removeIngredient})
{
    const [fetchedIngredients, setFetchedIngredients] = useState([]);
    const [fetchIngredientName, setFetchIngredientName] = useState(null);

    const initialLockedIngredientName = (initialIngredient.ingredientName === "") ? null : initialIngredient.ingredientName;
    const [lockedIngredientName, setLockedIngredientName] = useState(initialLockedIngredientName);

    useEffect(() =>
    {
        if (fetchIngredientName === null) return;

        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients(null, fetchIngredientName).then(result => {
            setFetchedIngredients(result);
        })
    }, [fetchIngredientName]);

    function handleEnteringIngredientName(e)
    {
        setFetchIngredientName(e.target.value)
    }

    const nameId = `ingredientName${i}`;
    const amountId = `ingredientAmount${i}`;
    const unitId = `ingredientUnit${i}`;

    const fetchedIngredientList = (fetchedIngredients.length !== 0) ? <ul>
        {fetchedIngredients.map(ing => {
            return <li key={ing.id} onClick={() => handleChooseIngredient(ing.id, ing.name)}>
                {ing.name}
            </li>;
        })}
    </ul> : "";

    function handleAddNewIngredient(inputId)
    {
        const value = document.getElementById(inputId).value;
        if (value === "") return;

        setLockedIngredientName(value);
    }

    function handleChooseIngredient(id, name)
    {
        setLockedIngredientName(name);
    }

    return (<>
        <div>
            {lockedIngredientName === null && (<>
            <div>
                <label htmlFor={nameId}>Name:</label>
                <input id={nameId} name={nameId} type="text" className="mr-2"
                        defaultValue={initialIngredient.ingredientName}
                        onInput={handleEnteringIngredientName} />
                <button type="button" onClick={() => handleAddNewIngredient(nameId)}>Add</button>
                {fetchedIngredientList}
            </div>
            </>)}

            {lockedIngredientName !== null && (<>
            <input hidden id={nameId} name={nameId} type="text" defaultValue={lockedIngredientName} />

            <div className="flex justify-content-between align-items-center">
                <span className="ingredientChip">
                    {lockedIngredientName}
                </span>
            
                <div>
                    <label htmlFor={amountId}>Amount:</label>
                    <input id={amountId} name={amountId} type="number" defaultValue={initialIngredient.amount} />
                </div>

                <div>
                    <label htmlFor={unitId}>Unit:</label>
                    <select defaultValue={initialIngredient.unitId} id={unitId} name={unitId}>
                        <UnitSelectOptions units={units} />
                    </select>
                </div>

                <button type="button" onClick={() => removeIngredient(i)}>Remove</button>
            </div>

            </>)}

        </div>
    </>);
}