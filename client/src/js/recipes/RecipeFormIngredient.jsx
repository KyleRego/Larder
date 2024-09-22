import { useState } from "react";

import { GoPencil } from "react-icons/go";

import RecipeFormIngredientFormInputs from "./RecipeFormIngredientFormInputs";
import findUnitName from "../helpers/findUnitName";

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

    return <li key={ingredient.id} className="list-group-item">
        {editing === false
        ?   <div className="m-0 d-flex justify-content-between align-items-center">
                <div className="d-flex align-items-center column-gap-3">
                    <span className="">
                        {ingredient.name}
                    </span>

                    {ingredient.quantity !== undefined &&
                        <span>
                            {`${ingredient.quantity.amount} ${findUnitName(ingredient.quantity.unitId, units)}`}
                        </span>
                    }
                    
                    <GoPencil onClick={() => setEditing(true)} role="button" title="Edit recipe ingredient" className="w-5 h-5" />
                </div>
                
                <div role="button" className="btn btn-outline-danger btn-sm" title="Remove ingredient from recipe" onClick={handleRemoveIngredient}>
                    X
                </div>
            </div>
        :  <RecipeFormIngredientFormInputs ingredient={ingredient} units={units}
                                                            setEditing={setEditing}
                                            formRecipe={formRecipe} setFormRecipe={setFormRecipe} /> }
        </li>;
}
