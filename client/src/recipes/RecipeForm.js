import { useState } from "react";
import { v4 as uuidv4 } from "uuid";

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
                                            recipeIngredient={recipeIngredient}
                                            i={i}
                                            units={units}
                                            removeIngredient={removeIngredient} />
    });

    return (
        <>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="recipeName"><strong>Recipe:</strong></label>
                    <input id="recipeName" name="recipeName" type="text" defaultValue={recipe.recipeName} />
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

function RecipeIngredientFormItem({recipeIngredient, i, units, removeIngredient})
{
    const nameId = `ingredientName${i}`;
    const amountId = `ingredientAmount${i}`;
    const unitId = `ingredientUnit${i}`;

    const unitOptions = UnitOptions(units);

    return (<>
        <strong>Ingredient {i}:</strong>

        <div>
            <div>
                <label htmlFor={nameId}>Name:</label>
                <input id={nameId} name={nameId} type="text" defaultValue={recipeIngredient.ingredientName} />
            </div>

            <div>
                <label htmlFor={amountId}>Amount:</label>
                <input id={amountId} name={amountId} type="number" defaultValue={recipeIngredient.amount} />
            </div>

            <div>
                <label htmlFor={unitId}>Unit:</label>
                <select defaultValue={recipeIngredient.unitId} id={unitId} name={unitId}>
                    {unitOptions}
                </select>
            </div>

            <button type="button" onClick={() => removeIngredient(i)}>Remove</button>

        </div>
    </>);
}

function UnitOptions(units)
{
    return units.map(u => {
        return <option key={u.id} value={u.id}>{u.name}</option>
    });
}