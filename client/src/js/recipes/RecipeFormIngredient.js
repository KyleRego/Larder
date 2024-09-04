import { useState } from "react";

import { GoPencil } from "react-icons/go";

import RecipeFormIngredientFormInputs from "./RecipeFormIngredientFormInputs";

export default function RecipeFormIngredient({ingredient, formRecipe, setFormRecipe, units})
{
    const [editing, setEditing] = useState(false);

    function handleRemoveIngredient()
    {
        let newFormRecipe = structuredClone(formRecipe);
        let newIngredients = [];

        for (let i = 0; i < newFormRecipe.ingredients.length; i += 1) {
            if (newFormRecipe.ingredients[i].id !== ingredient.id) {
                newIngredients.push(newFormRecipe.ingredients[i]);
            }
        }
        newFormRecipe.ingredients = newIngredients;
        setFormRecipe(newFormRecipe);
    }

    return <div className="border">
        {editing === false
        ?   <div className="d-flex column-gap-3 align-items-center">
                <span className="m-0">
                    {ingredient.name}
                </span>

                <span role="button" title="Edit recipe ingredient" className="" onClick={() => setEditing(true)}>
                    <GoPencil className="w-4 h-4" />
                </span>

                <span role="button" className="btn btn-outline-danger btn-sm" title="Remove ingredient from recipe" onClick={handleRemoveIngredient}>
                    X
                </span>
            </div>
        :  <RecipeFormIngredientFormInputs ingredient={ingredient} units={units}
                                                            setEditing={setEditing}
                                            formRecipe={formRecipe} setFormRecipe={setFormRecipe} /> }
        </div>;
}
