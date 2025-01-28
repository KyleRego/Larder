import { Link, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { ItemDto } from "../types/ItemDto";
import { NutritionInputs } from "../components/NutritionInputs";
import QuantityInput from "../components/QuantityInput";
import { useApiRequest } from "../hooks/useApiRequest";
import { NutritionDto } from "../types/NutritionDto";
import { QuantityDto } from "../types/QuantityDto";

export default function NewItem() {
    const [item, setItem] = useState<ItemDto>({
        id: null, name: "", amount: 1, description: null,
        nutrition: null, ingredient: null, quantity: null
    });

    useEffect(() => {
        console.log("Updated item:", item);
    }, [item]);

    function updateItem<K extends keyof ItemDto>(field: K, value: ItemDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, food: prevItem.nutrition, [field]: value }
        });
    }

    function updateItemQuantity<Q extends keyof QuantityDto>(
        field: Q,
        value: QuantityDto[Q]
        ) {
        setItem((prevItem) => ({
            ...prevItem,
            quantity: {
            ...prevItem.quantity ?? { id: null, amount: 0, unitId: null, unitName: null },
            [field]: value,
            }
        }));
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

    async function handleSubmit(e : React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const res = await handleRequest<ItemDto>({
            method: "post",
            url: "/api/Items",
            data: item
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
                        <input type="checkbox" className="btn-check" id="is-food-toggle" autoComplete="off"
                            title="Food"
                            checked={item.nutrition !== null} onChange={(e) => {
                                if (e.target.checked) {
                                    // Instantiating initial food may need to be moved somewhere to be reused 
                                    const initialFood: NutritionDto = {
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
                                        gramsTotalCarbs: 0
                                    };
                                    updateItem("nutrition", initialFood);
                                    console.log(item);
                                } else {
                                    updateItem("nutrition", null)
                                }

                            }} />
                        <label className="btn btn-outline-primary" htmlFor="is-food-toggle">Food</label>

                        <input type="checkbox" className="btn-check" id="is-ingredient-toggle" autoComplete="off"
                                title="Ingredient"
                                checked={checkboxStates.isIngredient} onChange={handleCheckboxChange} />
                        <label className="btn btn-outline-primary" htmlFor="is-ingredient-toggle">Ingredient</label>
                    </div>
                </div>
            </div>
            
            <div className="container">
                <form onSubmit={handleSubmit}>
                    <div className="new-element p-2">
                        <div className="d-flex align-items-center column-gap-3">
                            <div className="flex-grow-1">
                                <label htmlFor="name-input">Name:</label>
                                <input className="form-control" id="name-input" type="text" name="name" title="Item name:" required
                                    value={item.name} onChange={(e) => updateItem("name", e.target.value)} />
                            </div>

                            <div className="flex-grow-1">
                                <label htmlFor="amount-input">Quantity:</label>
                                <QuantityInput quantity={item.quantity}
                                    handleAmountChange={(e) => updateItemQuantity("amount", parseFloat(e.target.value))}
                                    handleUnitChange={(e) => updateItemQuantity("unitId", e.target.value)} />
                            </div>
                        </div>
    
                        <div className="mt-2">
                            <label htmlFor="description-input">Description:</label>
                            <textarea className="form-control" rows={1} id="description-input" name="description" title="Item description:"
                                value={item.description ?? ""} onChange={(e) => updateItem("description", e.target.value)} />
                        </div>
                    </div>

                    {item.nutrition !== null && (
                    <>
                        <div className="new-element border border-black p-4 my-4">
                            <NutritionInputs item={item} setItem={setItem} />
                        </div>
                    </>)}

                    <div className="mt-4 d-flex justify-content-center">
                        <button id="submit-new-item"
                                type="submit"
                                className="btn btn-primary">
                            Create item
                        </button>
                    </div>
                </form>
            </div>
        </>
    );
}
