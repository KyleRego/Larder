import { useState } from "react";

import { v4 as uuidv4 } from 'uuid';

import { GoPlus } from "react-icons/go";

import RecipeFormIngredient from "./RecipeFormIngredient";

export default function RecipeForm({recipe, units, handleSubmit})
{
    const [formRecipe, setFormRecipe] = useState(structuredClone(recipe));

    function addIngredient()
    {
        const newRecipe = structuredClone(formRecipe);
        const newIngredient = {
            id: uuidv4(),
            name: "New ingredient",
            quantity: {
                amount: 0
            }
        };
        newRecipe.ingredients.push(newIngredient);
        setFormRecipe(newRecipe);
    }

    let recipeFormIngredients = formRecipe.ingredients.map(ing => {
        return  <RecipeFormIngredient   key={ing.id}
                                        ingredient={ing}
                                        units={units}
                                        formRecipe={formRecipe}
                                        setFormRecipe={setFormRecipe}/>
    });

    return (
        <form onSubmit={(e) => handleSubmit(e, formRecipe)}>
            <div className="mb-4 d-flex flex-wrap column-gap-3 row-gap-1 align-items-center">
                <label htmlFor="recipeName">Name:</label>
                <input required className="flex-grow-1" id="recipeName" name="recipeName" type="text" defaultValue={recipe.name} />
            </div>

            <h2 className="mb-4">Ingredients:</h2>

            <ul className="list-group">
                {recipeFormIngredients}

                <li className="list-group-item d-flex justify-content-center">
                    <button type="button"
                                className="btn btn-secondary btn-sm"
                                onClick={addIngredient}>
                                    <GoPlus className="w-5 h-5" />
                                </button>
                </li>
            </ul>

            <div className="mt-4">
                <button className="btn btn-primary" type="submit">Submit</button>
            </div>
        </form>
    );
}
