import { useState } from "react";
import { Link } from "react-router-dom";
import "./NewRecipe.css";

export default function NewRecipe()
{
    function handleSubmit(e)
    {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);
        console.log(formData);
        // fetch('/some-api', { method: form.method, body: formData });
    }

    function addIngredient()
    {
        setIngredients([...ingredients, { name: "" }]);
    }

    function removeIngredient(index)
    {
        const newIngredients = [];
        console.log(index);
        for (let i = 0; i < ingredients.length; i += 1)
        {
            if (i !== index) newIngredients.push(ingredients[i]);
        }

        setIngredients(newIngredients);
    }

    const [ingredients, setIngredients] = useState([{name: "Ingredient 1"}]);

    let ingredientFormListItems = ingredients.map((ing, index) => RecipeIngredientFormItem(ing, index, removeIngredient));

    return (<>
        <h1>New recipe:</h1>

        <form method="post" onSubmit={handleSubmit}>
        <label>
            <strong>Recipe name:</strong> <input name="nameInput" defaultValue="New recipe" type="text" />
        </label>

        <h2>
            Ingredients:
        </h2>

        {ingredientFormListItems}

        <span className="recipeFormBtn" onClick={addIngredient}>Add ingredient</span>

        <div className="mt-4">
            <button type="reset">Reset form</button>
            <button type="submit">Submit form</button>
        </div>

        </form>

        <Link to={"/recipes"}>Back to recipes</Link>
    </>);
}

function RecipeIngredientFormItem(ingr, index, removeIngredient)
{
    const nameId = `name-input-${index}`;
    const amountId = `amount-input-${index}`;
    const unitId = `unit-input-${index}`;

    return (
        <div className="flex justify-content-between align-items-baseline mb-2" key={index}>
            <div className="flex flex-col flex-grow-1 mr-2">
                <input id={nameId} name="ingredientInput" defaultValue="" type="text" />

                <label className="text-center" htmlFor={nameId}>
                    <strong>Name</strong>
                </label>
            </div>

            <div className="flex flex-col mr-2">
                <input id={amountId} name="ingredientInput" defaultValue="" type="number" />

                <label className="text-center" htmlFor={amountId}>
                    <strong>Amount</strong>
                </label>
            </div>

            <div className="flex flex-col mr-2">
                <select id={unitId} name="ingredientInput" defaultValue="" type="text" >
                    <option value="someOption">Some option</option>
                    <option value="otherOption">Other option</option>
                </select>

                <label className="text-center" htmlFor={unitId}>
                    <strong>Unit</strong>
                </label>
            </div>

            <span className="recipeFormBtn" onClick={() => removeIngredient(index)}>Remove</span>
        </div>
    )
}