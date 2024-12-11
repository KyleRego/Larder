import { Dispatch, SetStateAction, useEffect } from "react";
import { foodNutritionData } from "../data/foodNutritionData";
import { ItemDto } from "../types/ItemDto";
import QuantityInput from "./QuantityInput";
import { FoodDto } from "../types/FoodDto";
import { QuantityComponentDto } from "../types/QuantityComponentDto";
import { QuantityDto } from "../types/QuantityDto";

export function FoodStockFormControls({item, setItem}
    : {item: ItemDto, setItem: Dispatch<SetStateAction<ItemDto>>}) {

    function updateItemFood<K extends keyof FoodDto>(field: K, value: FoodDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, food: { ...prevItem.food, [field]: value } } as ItemDto;
        });
    }

    function updateItemFoodServingSize<K extends keyof QuantityDto>(field: K, value: QuantityDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem,
                    food: { ...prevItem.food,
                            servingSize: { ...prevItem.food?.servingSize, [field] : value} } } as ItemDto;
        });
    }

    function updateItemQuantity<K extends keyof QuantityComponentDto>(field: K, value: QuantityComponentDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, quantityComp: { ...prevItem.quantityComp, [field]: value } } as ItemDto;
        })
    }

    useEffect(() => {
        const numServings = item.amount * item.food!.servingsPerItem;

        updateItemFood("servings" as keyof FoodDto, numServings);

    }, [item.amount, item.food!.servingsPerItem]);

    useEffect(() => {
        const newAmount = item.food!.servings * item.food!.servingSize.amount;
        const newQuantity : QuantityDto = {
            id: null, unitName: null, amount: newAmount,
            unitId: item.quantityComp?.quantity?.unitId ?? null
        };

        updateItemQuantity("quantity" as keyof QuantityComponentDto, newQuantity);
    }, [item.food!.servings]);

    useEffect(() => {
        const newAmountPerItem = item.food!.servingsPerItem * item.food!.servingSize.amount;
        const newUnit = item.food!.servingSize.unitId;

        const newQuantityPerItem: QuantityDto = {id: null, unitName: null, amount: newAmountPerItem, unitId: newUnit};

        updateItemQuantity("quantityPerItem" as keyof QuantityComponentDto, newQuantityPerItem);
    }, [item.food!.servingSize, item.food?.servingsPerItem]);

    useEffect(() => {
        if (!item.quantityComp?.quantityPerItem) return;

        const newAmount = item.amount * item.quantityComp!.quantityPerItem!.amount;
        const newUnit = item.quantityComp?.quantityPerItem.unitId ?? "";
        const newQuantity : QuantityDto = {id: null, unitName: null, amount: newAmount, unitId: newUnit };

        updateItemQuantity("quantity" as keyof QuantityComponentDto, newQuantity);
    }, [item.quantityComp?.quantityPerItem])

    return (<>
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
            <QuantityInput title="Serving size" name="servingSize" quantity={item.food!.servingSize}
                onChangeAmount={(e) => updateItemFoodServingSize("amount", parseFloat(e.target.value))}
                onChangeUnit={(e) => updateItemFoodServingSize("unitId", e.target.value)} />
        </div>
    </>)
}

export function FoodNutritionFormControls({item, setItem}
        : {item: ItemDto, setItem: Dispatch<SetStateAction<ItemDto>> }) {

    function updateItemFood<K extends keyof FoodDto>(field: K, value: FoodDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, food: { ...prevItem.food, [field]: value } } as ItemDto;
        });
    }

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
            <h3>Nutrition per item 🍑</h3>

            {nutritionInputs}
        </> 
    );
}
