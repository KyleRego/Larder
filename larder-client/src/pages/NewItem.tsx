import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import { ItemDto } from "../types/Item";
import { FoodDto } from "../types/FoodDto";
import FoodFormControls from "../components/FoodFormControls";
import QuantityComponentFormControls from "../components/IngredientFormControls";
import { useApiRequest } from "../hooks/useApiRequest";

export default function NewItem() {
    const { handleRequest } = useApiRequest();
    const navigate = useNavigate();
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

    async function handleSubmit(e : React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);

        let newItem: ItemDto = {
            id: null,
            name: formData.get("name") as string,
            amount: formData.get("amount") as string,
            description: formData.get("description") as string,
            food: null,
            ingredient: null
        };

        if (checkboxStates.isFood === true) {
            let food: FoodDto = {
                calories: parseFloat(formData.get("calories") as string),
                servings: parseFloat(formData.get("servings") as string),
                servingSize: {
                    id: null, unitName: null,
                    amount: parseFloat(formData.get("servingSize") as string),
                    unitId: formData.get("servingSizeUnit") as string
                }
            }

            newItem.food = food;
        }

        const res = await handleRequest<ItemDto>({
            method: "post",
            url: "/api/Items",
            data: newItem
        });

        if (res) {
            navigate("/items");
        }
    }

    return (
        <>
            <div className="page-flex-header">
                <h1>New item:</h1>

                <div>
                    <div>
                        <strong>Item is a/an:</strong>
                    </div>
                    <div className="btn-group" role="group">
                        <input type="checkbox" className="btn-check" id="isFood" name="isFood" autoComplete="off"
                            checked={checkboxStates.isFood} onChange={handleCheckboxChange} />
                        <label className="btn btn-outline-primary" htmlFor="isFood">Food</label>

                        <input type="checkbox" className="btn-check" id="isIngredient" name="isIngredient" autoComplete="off"
                                checked={checkboxStates.isIngredient} onChange={handleCheckboxChange} />
                        <label className="btn btn-outline-primary" htmlFor="isIngredient">Ingredient</label>
                    </div>
                </div>

                <Link className="btn btn-danger" to={"/items"}>Cancel</Link>
            </div>
            
            <div className="mt-4 container">
                <form onSubmit={handleSubmit}>
                    <div>
                        <div className="d-flex align-items-center column-gap-3">
                            <div className="flex-grow-1">
                                <label htmlFor="nameInput">Name:</label>
                                <input className="form-control" id="nameInput" type="text" name="name" title="Item name:" required></input>
                            </div>

                            <div className="flex-grow-1">
                                <label htmlFor="amountInput">Amount:</label>
                                <input className="form-control" type="number" id="amountInput" name="amount" title="Item amount:" required></input>
                            </div>
                        </div>
                        
                        <div className="mt-2">
                            <label htmlFor="descriptionInput">Description:</label>
                            <textarea className="form-control" rows={1} id="descriptionInput" name="description" title="Item description:"></textarea>
                        </div>
                    </div>

                    {(checkboxStates.isFood || checkboxStates.isIngredient) && (
                    <>
                        <div className="mt-4">
                            <QuantityComponentFormControls item={null} />
                        </div>
                    </>)}

                    {checkboxStates.isFood && (
                    <>
                        <div className="mt-4">
                            <FoodFormControls item={null} />
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
