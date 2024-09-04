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
            name: "New ingredient"
        };
        newRecipe.ingredients.push(newIngredient);
        setFormRecipe(newRecipe);
    }

    let recipeFormIngredients = formRecipe.ingredients.map((ing, i) => {
        return <RecipeFormIngredient    key={ing.id}
                                            ingredient={ing}
                                            units={units}
                                            formRecipe={formRecipe}
                                            setFormRecipe={setFormRecipe}/>
    });

    return (
        <form className="recipeForm" onSubmit={handleSubmit}>
            <div className="d-flex flex-wrap column-gap-1 row-gap-1 align-items-center">
                <label htmlFor="recipeName">Name:</label>
                <input className="flex-grow-1" id="recipeName" name="recipeName" type="text" defaultValue={recipe.name} />
            </div>

            <h2 className="m-0">Ingredients:</h2>

            <div className="flex column-gap-5">
                {recipeFormIngredients}
            </div>
           
            <div className="d-flex justify-content-center">
                <button type="button"
                            className="btn btn-secondary btn-sm"
                            onClick={addIngredient}>
                                <GoPlus className="w-5 h-5" />
                            </button>
            </div>

            <div>
                <button className="btn btn-primary" type="submit">Submit</button>
            </div>
        </form>
    );
}
