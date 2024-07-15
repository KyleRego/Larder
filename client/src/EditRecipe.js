import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { v4 as uuidv4 } from "uuid";

import RecipesService from "./services/RecipesService";
import UnitsService from "./services/UnitsService";

import "./EditRecipe.css"

export default function EditRecipe()
{
    function removeRecipeIngredient(index)
    {
        const newRecipeIngredients = [];

        recipe.recipeIngredients.forEach((ri, i) => {
            if (index !== i)
            {
                newRecipeIngredients.push(ri);
            }
        });

        const newRecipe = structuredClone(recipe);
        newRecipe.recipeIngredients = newRecipeIngredients;
        setRecipe(newRecipe);
    }

    function addRecipeIngredient()
    {
        const newRecipe = structuredClone(recipe);
        newRecipe.recipeIngredients.push({
            id: uuidv4(),
            amount: 0,
            ingredient: {
                id: null,
                name: ""
            },
            unit: {
                id: null,
                name: ""
            }
        });
        setRecipe(newRecipe);
    }

    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const recipeData = {recipeId: recipe.id};
        const ingredientsData = [];

        formData.entries().forEach(entry => {
            const key = entry[0];
            const val = entry[1];

            if (key === "recipeName")
            {
                recipeData["recipeName"] = val;
            }
            else if (key.startsWith("ingredient"))
            {
                const index = key.substring(key.length - 1);

                if (!ingredientsData[index])
                {
                    ingredientsData[index] = {};
                }

                const restOfKey = key.substring("ingredient".length)

                if (restOfKey.startsWith("Name"))
                {
                    ingredientsData[index]["ingredientName"] = val;
                }
                else if (restOfKey.startsWith("Amount"))
                {
                    ingredientsData[index]["amount"] = val;
                }
                else if (restOfKey.startsWith("Unit"))
                {
                    ingredientsData[index]["unitId"] = val;
                }
            }
        });

        recipeData["ingredients"] = ingredientsData;

        console.log(recipeData);

        const recipesService = new RecipesService();

        await recipesService.putRecipe(recipeData);
    }

    let { id } = useParams();
    const [recipe, setRecipe] = useState(null);
    const [units, setUnits] = useState(null);

    useEffect(() =>
    {
        const recipesService = new RecipesService();

        recipesService.getRecipe(id).then(result => {
            setRecipe(result);
        });

        const unitsService = new UnitsService();

        unitsService.getUnits().then(result => {
            setUnits(result);
        })
    }, [id]);

    if (recipe === null || units === null) return <h1>Loading...</h1>;

    return (
        <>
            <h1>Editing recipe: {recipe.name}</h1>

            <RecipeForm recipe={recipe} units={units}
                        removeRecipeIngredient={removeRecipeIngredient}
                        addRecipeIngredient={addRecipeIngredient}
                        handleSubmit={handleSubmit} />

            <Link to={`/recipes/${id}`}>Back to recipe</Link>
        </>
    );
}

function RecipeForm({recipe, units, removeRecipeIngredient, addRecipeIngredient, handleSubmit})
{
    let recipeIngredientFormItems = recipe.recipeIngredients.map((ri, i) => {
        return <RecipeIngredientFormItem key={ri.id} ri={ri} i={i} units={units} removeRecipeIngredient={removeRecipeIngredient} />
    });

    return (
        <>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="recipeName"><strong>Recipe:</strong></label>
                    <input id="recipeName" name="recipeName" type="text" defaultValue={recipe.name} />
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

function RecipeIngredientFormItem({ri, i, units, removeRecipeIngredient})
{
    const nameId = `ingredientName${i}`;
    const amountId = `ingredientAmount${i}`;
    const unitId = `ingredientUnit${i}`;

    const unitOptions = UnitOptions(units)

    return (<>
        <strong>Ingredient {i}:</strong>
        
        <div>
            <div>
                <label htmlFor={nameId}>Name:</label>
                <input id={nameId} name={nameId} type="text" defaultValue={ri.ingredient.name} />
            </div>

            <div>
                <label htmlFor={amountId}>Amount:</label>
                <input id={amountId} name={amountId} type="number" defaultValue={ri.amount} />
            </div>

            <div>
                <label htmlFor={unitId}>Unit:</label>
                <select id={unitId} name={unitId} defaultValue={ri.unit.name}>
                    {unitOptions}
                </select>
            </div>
            
            <button type="button" onClick={() => removeRecipeIngredient(i)}>Remove</button>
            
        </div>
    </>);
}

function UnitOptions(units)
{
    if (units === null) return;

    return units.map(u => {
        return <option value={u.id}>
            {u.name}
        </option>
    })
}