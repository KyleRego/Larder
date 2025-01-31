import { useState } from "react";
import { ItemDto } from "../types/ItemDto";
import { NutritionDto } from "../types/NutritionDto";
import QuantityInput from "./QuantityInput";
import { QuantityDto } from "../types/QuantityDto";
import NutritionInput from "./NutritionInput";

export default function ItemForm({initialItem, submitFormItem}
        : {initialItem: ItemDto,
            submitFormItem: (newItem: ItemDto) => void}) {

    const [item, setItem] = useState<ItemDto>(initialItem);

    function initialNewNutrition() : NutritionDto {
        return {
            servingSize: { amount: 1, unitId: null, unitName: null },
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
        }
    }

    function handleNutritionToggle(e: React.FormEvent<HTMLInputElement>) {
        if (!e.currentTarget.checked) {
            setItem( { ...item, nutrition: null })
        } else {
            setItem( { ...item, nutrition: initialNewNutrition()})
        }
    }

    function setName(name: string) {
        setItem({...item, name: name});
    }

    function setDescription(description: string) {
        setItem({...item, description: description});
    }

    function setQuantity(quantity: QuantityDto) {
        setItem({...item,
            quantity: { amount: quantity.amount,
                        unitId: quantity.unitId,
                        unitName: quantity.unitName}
        });
    }

    function setNutrition(nutrition: NutritionDto) {
        setItem({...item,
            nutrition: nutrition
        })
    }

    async function handleSubmit(e : React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        submitFormItem(item);
    }

    return <div>
        <div>
            <div className="mb-2"><strong>Item components:</strong></div>
            <div className="btn-group" role="group">
                <input type="checkbox" className="btn-check" id="is-nutrition-toggle"
                            autoComplete="off" title="Nutrition"
                            checked={item.nutrition !== null} onChange={handleNutritionToggle} />
                <label className="btn btn-outline-primary"
                        htmlFor="is-nutrition-toggle">
                    Nutrition
                </label>
            </div>
        </div>

        <div className="my-4">
            <form id="item-form" onSubmit={handleSubmit}>
                <div className="d-flex align-items-center column-gap-3 mb-2">
                    <div className="input-group w-50">
                        <span className="input-group-text">Name</span>
                        <input type="text" className="form-control" aria-label="name"
                            onChange={(e) => setName(e.currentTarget.value)}
                            required id="name-input" />
                    </div>
                    <div className="flex-grow-1">
                        <QuantityInput quantityLabel="Quantity" initialQuantity={item.quantity}
                                handleQuantityChange={setQuantity} />
                    </div>
                </div>
                
                <div className="input-group mb-2">
                    <span className="input-group-text">Description</span>
                    <textarea className="form-control" aria-label="description" rows={1}
                        onChange={(e) => setDescription(e.currentTarget.value)}
                        id="description-input" />
                </div>

                { item.nutrition && 
                    <div className="my-4">
                        <NutritionInput initialNutrition={item.nutrition}
                        handleNutritionChange={setNutrition} />
                    </div>  
                }
            </form>
        </div>
    </div>;
}