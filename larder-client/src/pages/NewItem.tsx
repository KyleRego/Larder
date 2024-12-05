import { Link, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { ItemDto } from "../types/ItemDto";
import FoodFormControls from "../components/FoodFormControls";
import QuantityComponentFormControls from "../components/QuantityComponentFormControls";
import { useApiRequest } from "../hooks/useApiRequest";
import { FoodDto } from "../types/FoodDto";

export default function NewItem() {
    const [item, setItem] = useState<ItemDto>({
        id: null, name: "", amount: 1, description: null,
        food: null, ingredient: null, quantityComp: null
    });

    useEffect(() => {
        console.log("Updated item:", item);
    }, [item]);

    function updateItem<K extends keyof ItemDto>(field: K, value: ItemDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, food: prevItem.food, [field]: value }
        });
    }

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

    function itemNeedsQuantity(): boolean {
        return item.food !== null || item.ingredient !== null
    }

    async function handleSubmit(e : React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        // const formData = new FormData(e.currentTarget);

        let newItem = item;
        // let newItem: ItemDto = {
        //     id: null,
        //     name: formData.get("name") as string,
        //     amount: formData.get("amount") as string,
        //     description: formData.get("description") as string,
        //     food: null,
        //     ingredient: null,
        //     quantityComp: null
        // };

        // if (itemHasQuantity()) {
        //     let quantityComp: QuantityComponentDto = {
        //         quantity: {
        //             id: null, unitName: null,
        //             amount: parseFloat(formData.get("quantity") as string),
        //             unitId: formData.get("quantityUnit") as string
        //         },
        //         quantityPerItem: {
        //             id: null, unitName: null,
        //             amount: parseFloat(formData.get("quantityPerItem") as string),
        //             unitId: formData.get("quantityPerItemUnit") as string
        //         }
        //     };

        //     newItem.quantityComp = quantityComp;
        // }

        // if (checkboxStates.isFood === true) {
        //     let food: FoodDto = {
        //         calories: parseFloat(formData.get("calories") as string),
        //         servings: parseFloat(formData.get("servings") as string),
        //         servingSize: {
        //             id: null, unitName: null,
        //             amount: parseFloat(formData.get("servingSize") as string),
        //             unitId: formData.get("servingSizeUnit") as string
        //         },
        //         gramsProtein: parseFloat(formData.get("gramsProtein") as string),
        //         gramsTotalFat: parseFloat(formData.get("gramsTotalFat") as string),
        //         gramsSaturatedFat: parseFloat(formData.get("gramsSaturatedFat") as string),
        //         gramsTransFat: parseFloat(formData.get("gramsTransFat") as string),
        //         milligramsCholesterol: parseFloat(formData.get("milligramsCholesterol") as string),
        //         milligramsSodium: parseFloat(formData.get("milligramsSodium") as string),
        //         gramsTotalCarbs: parseFloat(formData.get("gramsTotalCarbs") as string),
        //         gramsDietaryFiber: parseFloat(formData.get("gramsDietaryFiber") as string),
        //         gramsTotalSugars: parseFloat(formData.get("gramsTotalSugars") as string),
        //         totalCalories: 0,
        //         totalGramsProtein: 0
        //     }

        //     newItem.food = food;
        // }

        // if (checkboxStates.isIngredient === true) {
        //     let ingredient: IngredientDto = {
        //         id: null
        //     };

        //     newItem.ingredient = ingredient;
        // }

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
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item active" aria-current="page">
                        <Link to={"/items"}>Back to items</Link>
                    </li>
                </ol>
            </nav>

            <div className="page-flex-header">
                <h1>New item 🤔</h1>

                <div>
                    <div>
                        <strong>Item is a/an:</strong>
                    </div>
                    <div className="btn-group" role="group">
                        <input type="checkbox" className="btn-check" id="isFood" name="isFood" autoComplete="off"
                            checked={checkboxStates.isFood} onChange={(e) => {
                                if (e.target.checked) {
                                    // Instantiating initial food may need to be moved somewhere to be reused 
                                    const initialFood: FoodDto = {
                                        servingsPerItem: 1,
                                        servings: 1 * item.amount,
                                        servingSize: { id: null, amount: 0, unitId: null, unitName: null },
                                        calories: 0,
                                        gramsProtein: 0,
                                        gramsTotalFat: 0,
                                        gramsSaturatedFat: 0,
                                        gramsTransFat: 0,
                                        milligramsCholesterol: 0,
                                        milligramsSodium: 0,
                                        gramsDietaryFiber: 0,
                                        gramsTotalSugars: 0,
                                        gramsTotalCarbs: 0,
                                        totalCalories: 0,
                                        totalGramsProtein: 0,
                                    };
                                    updateItem("food", initialFood);
                                    console.log(item);
                                } else {
                                    updateItem("food", null)
                                }
                                if (!itemNeedsQuantity()) {
                                    updateItem("quantityComp", null);
                                }
                            }} />
                        <label className="btn btn-outline-primary" htmlFor="isFood">Food</label>

                        <input type="checkbox" className="btn-check" id="isIngredient" name="isIngredient" autoComplete="off"
                                checked={checkboxStates.isIngredient} onChange={handleCheckboxChange} />
                        <label className="btn btn-outline-primary" htmlFor="isIngredient">Ingredient</label>
                    </div>
                </div>
            </div>
            
            <div className="mt-4 container">
                <form onSubmit={handleSubmit}>
                    <div className="new-element border border-black p-4">
                        <h3>Basic details ✅</h3>
                        <div className="d-flex align-items-center column-gap-3">
                            <div className="flex-grow-1">
                                <label htmlFor="nameInput">Name:</label>
                                <input className="form-control" id="nameInput" type="text" name="name" title="Item name:" required
                                    value={item.name} onChange={(e) => updateItem("name", e.target.value)} />
                            </div>

                            <div className="flex-grow-1">
                                <label htmlFor="amountInput">Amount:</label>
                                <input className="form-control" type="number" id="amountInput" name="amount" title="Item amount:" required
                                    value={item.amount} onChange={(e) => updateItem("amount", parseFloat(e.target.value))} />
                            </div>
                        </div>
    
                        <div className="mt-2">
                            <label htmlFor="descriptionInput">Description:</label>
                            <textarea className="form-control" rows={1} id="descriptionInput" name="description" title="Item description:"
                                value={item.description ?? ""} onChange={(e) => updateItem("description", e.target.value)} />
                        </div>
                    </div>

                    {item.food !== null && (
                    <>
                        <div className="new-element border border-black p-4 my-4">
                            <FoodFormControls item={item} setItem={setItem} />
                        </div>
                    </>)}

                    {(itemNeedsQuantity() === true) && (
                    <>
                        <div className="new-element border border-black p-4 mt-4">
                            <h3>Quantity ⚖️</h3>
                            <QuantityComponentFormControls item={null} />
                        </div>
                    </>)}

                    <div className="mt-4 d-flex justify-content-center">
                        <button id="submit-new-item" type="submit" className="btn btn-primary">Create item</button>
                    </div>
                </form>
            </div>
        </>
    );
}
