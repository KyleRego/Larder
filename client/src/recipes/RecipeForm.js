import { useState, useEffect } from "react";
import { v4 as uuidv4 } from "uuid";

import UnitSelectOptions from "../units/UnitSelectOptions";

import IngredientsService from "../services/IngredientsService";

import "./RecipeForm.css";

export default function RecipeForm({initialRecipe, units, handleSubmit})
{
    const [recipe, setRecipe] = useState(initialRecipe);
    const [addingIngredient, setAddingIngredient] = useState(false);

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

    function addIngredient(ingredient)
    {
        const newRecipe = structuredClone(recipe);
        newRecipe.ingredients.push(ingredient);
        setRecipe(newRecipe);
    }

    function showNewRecipeFormIngredient()
    {
        setAddingIngredient(true);
    }

    let recipeIngredients = recipe.ingredients.map((recipeIngredient, i) => {
        return <RecipeFormIngredient    key={recipeIngredient.recipeIngredientId}
                                            initialIngredient={recipeIngredient}
                                            i={i}
                                            units={units}
                                            removeIngredient={removeIngredient} />
    });

    return (
        <form className="recipeForm" onSubmit={handleSubmit}>
            <div className="flex align-items-center">
                <label htmlFor="recipeName"><strong>Recipe name:</strong></label>
                <input className="flex-grow-1" id="recipeName" name="recipeName" type="text" defaultValue={recipe.recipeName} />
            </div>

            <div className="flex column-gap-5">
                {recipeIngredients}
            </div>

            {addingIngredient === false && 
            <div>
                <button type="button" onClick={showNewRecipeFormIngredient}>New ingredient</button>
            </div>
            }

            {addingIngredient === true &&
            <div>
                <NewRecipeFormIngredient setAddingIngredient={setAddingIngredient} addIngredient={addIngredient} />
            </div>
            }

            <div>
                <button type="submit">Submit</button>
            </div>
        </form>
    );
}

function NewRecipeFormIngredient({setAddingIngredient, addIngredient})
{
    const [fetchedIngredients, setFetchedIngredients] = useState([]);
    const [fetchIngredientName, setFetchIngredientName] = useState(null);

    const startFetchingExistingIngredientOptions = (e) => setFetchIngredientName(e.target.value);

    function handleAddNewIngredient(inputId)
    {
        const value = document.getElementById(inputId).value;
        if (value === "") return;

        addIngredient({id: uuidv4(), ingredientName: value});
        setAddingIngredient(false);
    }

    function handleChooseIngredient(id, name)
    {
        addIngredient({id: id, ingredientName: name});
        setAddingIngredient(false);
    }

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
            return <li key={ing.id} onClick={() => handleChooseIngredient(ing.id, ing.name)}>
                {ing.name}
            </li>;
        })}
    </ul> : "";

    const inputId = "newIngredient";

    return <div>
                <label htmlFor={inputId}>Name:</label>
                <input id={inputId} type="text" className="mr-2" onInput={startFetchingExistingIngredientOptions} />
                <button type="button" onClick={() => handleAddNewIngredient(inputId)}>Add ingredient</button>
                {fetchedIngredientItems}
            </div>
} 

function RecipeFormIngredient({initialIngredient, i, units, removeIngredient})
{
    const nameId = `ingredientName${i}`;
    const amountId = `ingredientAmount${i}`;
    const unitId = `ingredientUnit${i}`;

    return (<>
        <div className="recipeFormIngredient">
            <div>
                <input hidden id={nameId} name={nameId} type="text" defaultValue={initialIngredient.ingredientName} />
                {initialIngredient.ingredientName}
            </div>

            <div>
                <label htmlFor={amountId}>Amount:</label>
                <input id={amountId} name={amountId} type="number" defaultValue={initialIngredient.amount} />
                <label hidden htmlFor={unitId}>Unit:</label>
                <select defaultValue={initialIngredient.unitId} id={unitId} name={unitId}>
                    <UnitSelectOptions units={units} />
                </select>
            </div>

            <div>
                <button type="button" onClick={() => removeIngredient(i)}>Remove</button>
            </div>

        </div>
    </>);
}