import { Dispatch, SetStateAction, useEffect } from "react";
import { foodNutritionData } from "../data/foodNutritionData";
import { ItemDto } from "../types/ItemDto";
import QuantityInput from "./QuantityInput";
import { FoodDto } from "../types/FoodDto";

export default function FoodFormControls({item, setItem}
        : {item: ItemDto, setItem: Dispatch<SetStateAction<ItemDto>> }) {

    function updateItemFood<K extends keyof FoodDto>(field: K, value: FoodDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, food: { ...prevItem.food, [field]: value } } as ItemDto;
        });
    }

    useEffect(() => {
        updateItemFood("servings" as keyof FoodDto, item.amount * item.food!.servingsPerItem)
    }, [item.amount, item.food!.servingsPerItem]);

    const nutritionInputs = foodNutritionData.map(entry => {
        const field = entry.field as keyof FoodDto;
        const label = entry.label;
        const unit = entry.unitName;

        const initialValue = item.food ? (item.food as any)[field] : 0;

        return <div className="row g-1 align-items-center mt-2" key={field}>
            <div className={`col-auto text-center  `}>
                <label style={{width: "108px"}} className={`col-form-label ${entry.emphasized === true ? "fw-bold" : ""}`} htmlFor={field}>{label}:</label>
            </div>
            <div className="col-auto">
                <input className="form-control" type="number" step="any" name={field} title={label}
                    id={`${field}-input`}
                    defaultValue={initialValue} onChange={(e) => updateItemFood(field, Number(e.target.value))} />
            </div>
            <div className="col-auto font-monospace">
                {unit}
            </div>
        </div>;
    });

    return (
        <>
            <h3>Food stock 🥫</h3>

            <div className="d-flex column-gap-3 mt-2">
                <div className="flex-grow-1">
                    <label className="form-label" htmlFor="servings-per-item-input">Servings per item:</label>
                    <input className="form-control" type="number" value={item.food!.servingsPerItem}
                        id="servings-per-item-input"
                        onChange={(e) => updateItemFood("servingsPerItem" as keyof FoodDto, parseFloat(e.target.value))} />
                </div>
                <div className="flex-grow-1">
                    <label className="form-label" htmlFor="servings-input">Servings:</label>
                    <input className="form-control" id="servings-input" type="number" name="servings" title="Food servings:"
                       value={String(item.food?.servings)} onChange={(e) => updateItemFood("servings", parseFloat(e.target.value))} required />
                </div>
            </div>

            <div className="mt-2">
                <label className="form-label">Serving size:</label>
                <QuantityInput title="Serving size" name="servingSize" initialQuantity={null} />
            </div>

            <h3 className="mt-4">Nutrition per item 🍑</h3>

            {nutritionInputs}
        </> 
    );
}
