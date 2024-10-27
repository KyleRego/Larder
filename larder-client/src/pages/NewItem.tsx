import { Link } from "react-router-dom";
import { apiClient } from "../util/axios";
import { useState } from "react";
import { Item } from "../types/Item";
import { Food } from "../types/Food";

export default function NewItem() {
    const [checkboxStates, setCheckboxStates] = useState({
        isFood: false,
        isIngredient: false
    });

    function handleCheckboxChange(e: React.ChangeEvent<HTMLInputElement>) {
        const { name, checked } = e.target;
        setCheckboxStates(prevState => ({
            ...prevState,
            [name]: checked
        }));
    };

    function handleSubmit(e : React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);

        let newItem: Item = {
            id: null,
            name: formData.get("name") as string,
            description: formData.get("description") as string,
            food: null,
            ingredient: null
        };

        if (checkboxStates.isFood === true) {
            let food: Food = {
                calories: parseFloat(formData.get("calories") as string),
                servings: parseFloat(formData.get("servings") as string)
            }

            newItem.food = food;
        }

        apiClient.post("/api/Items", newItem);
    }

    return <>
        <h1>
            New item:
        </h1>
        <div>
            <h2>Item is a?</h2>
            <div>
                <label>
                    <input type="checkbox" name="isFood"
                                            checked={checkboxStates.isFood}
                                            onChange={handleCheckboxChange} />
                    Food
                </label>
            </div>
            <div>
                <label>
                    <input
                        type="checkbox" name="isIngredient"
                                        checked={checkboxStates.isIngredient}
                                        onChange={handleCheckboxChange} />
                    Ingredient
                </label>
            </div>
        </div>
        
        <div>
            <form onSubmit={handleSubmit}>
                <div>
                    <div>
                        <label htmlFor="nameInput">Name:</label>
                        <input id="nameInput" type="text" name="name" title="Item name:" required></input>
                    </div>
                    <div>
                        <label htmlFor="descriptionInput">Description:</label>
                        <textarea id="descriptionInput" name="description" title="Item description:"></textarea>
                    </div>
                </div>

                {checkboxStates.isFood && (
                <>
                    <div>
                        <h3>Food information:</h3>
                        <div>
                            <label htmlFor="caloriesInput">Calories:</label>
                            <input id="caloriesInput" type="number" name="calories" title="Food calories:" />
                        </div>
                        <div>
                            <label htmlFor="servings">Servings:</label>
                            <input id="servings" type="number" name="servings" title="Food servings:" />
                        </div>
                    </div>
                </>)}

                <div className="mt-4">
                    <button type="submit" className="btn btn-primary">Create item</button>
                </div>
            </form>
        </div>
        <div>
            <Link to={"/"}>Home</Link>
        </div>
    </>;
}
