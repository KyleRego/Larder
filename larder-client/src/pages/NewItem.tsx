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

    return (
        <>
            <div className="page-flex-header">
                <h1>New item:</h1>
                <Link className="btn btn-danger" to={"/items"}>Cancel</Link>
            </div>
            
            <div className="mt-4">
                <form onSubmit={handleSubmit}>
                    <div>
                        <div>
                            <label htmlFor="nameInput">Name:</label>
                            <input id="nameInput" type="text" name="name" title="Item name:" required></input>
                        </div>
                        <div className="mt-2">
                            <label htmlFor="descriptionInput">Description:</label>
                            <textarea id="descriptionInput" name="description" title="Item description:"></textarea>
                        </div>
                    </div>

                    <div className="btn-group mt-4" role="group">
                        <input type="checkbox" className="btn-check" id="isFood" name="isFood" autoComplete="off"
                            checked={checkboxStates.isFood} onChange={handleCheckboxChange} />
                        <label className="btn btn-outline-primary" htmlFor="isFood">Food</label>

                        <input type="checkbox" className="btn-check" id="isIngredient" name="isIngredient" autoComplete="off"
                                checked={checkboxStates.isIngredient} onChange={handleCheckboxChange} />
                        <label className="btn btn-outline-primary" htmlFor="isIngredient">Ingredient</label>
                    </div>

                    {checkboxStates.isFood && (
                    <>
                        <div className="mt-4">
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

                    <div className="mt-4 d-flex justify-content-center">
                        <button type="submit" className="btn btn-primary">Create item</button>
                    </div>
                </form>
            </div>
        </>
    );
}
