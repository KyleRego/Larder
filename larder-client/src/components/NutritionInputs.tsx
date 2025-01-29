import { Dispatch, SetStateAction } from "react";
import { UINutrientData } from "../data/UINutrientData";
import { ItemDto } from "../types/ItemDto";
import { NutritionDto } from "../types/NutritionDto";
import QuantityInput from "./QuantityInput";

export function NutritionInputs({item, setItem}
        : {item: ItemDto, setItem: Dispatch<SetStateAction<ItemDto>> }) {

    function updateItemNutrition<K extends keyof NutritionDto>(field: K, value: NutritionDto[K]) {
        setItem((prevItem) => {
            return { ...prevItem, nutrition: { ...prevItem.nutrition, [field]: value } } as ItemDto;
        });
    }

    function updateServingAmount(e: React.ChangeEvent<HTMLInputElement>) {
        setItem((prevItem) => {
            return { ...prevItem, nutrition: { ...prevItem.nutrition,
                servingSize: { ...prevItem.nutrition?.servingSize,
                    amount: parseFloat(e.target.value)
                }}} as ItemDto;
        })
    }

    function updateServingUnit(e: React.ChangeEvent<HTMLSelectElement>) {
        setItem((prevItem) => {
            return { ...prevItem, nutrition: { ...prevItem.nutrition,
                servingSize: { ...prevItem.nutrition?.servingSize,
                    unitId: e.target.value as string
                }}} as ItemDto;
        })
    }

    const nutritionInputs = UINutrientData.map(entry => {
        const field = entry.field as keyof NutritionDto;
        const label = entry.label;
        const unit = entry.unitName;

        const initialValue = item.nutrition ? (item.nutrition as any)[field] : 0;

        return <div className="row g-1 align-items-center mt-2" key={field}>
            <div className={`col-auto text-center  `}>
                <label style={{width: "108px"}}
                        className={`col-form-label ${entry.emphasized === true ? "fw-bold" : ""}`}
                        htmlFor={field}>
                    {label}:
                </label>
            </div>
            <div className="col-auto">
                <input className="form-control"
                        type="number"
                        step="any"
                        name={field}
                        title={label}
                        id={`${field}-input`}
                        defaultValue={initialValue}
                        onChange={(e) => updateItemNutrition(field, Number(e.target.value))} />
            </div>
            <div className="col-auto font-monospace">
                {unit}
            </div>
        </div>;
    });

    return (
        <>
            <div className="d-flex justify-content-around align-items-center flex-wrap row-gap-3 mb-4">
                <h3 className="m-0">Nutrition 🍑</h3>

                <div className="d-flex column-gap-3 align-items-center">
                    <h4 className="m-0">Serving size:</h4>

                    <QuantityInput quantity={item.nutrition?.servingSize ?? null }
                                handleAmountChange={updateServingAmount}
                                handleUnitChange={updateServingUnit} />
                </div>
            </div>
            {nutritionInputs}
        </> 
    );
}
