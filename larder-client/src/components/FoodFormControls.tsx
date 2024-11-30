import { foodNutritionData } from "../data/foodNutritionData";
import { ItemDto } from "../types/Item";
import QuantityInput from "./QuantityInput";

export default function FoodFormControls({item} : {item: ItemDto | null}) {

    const nutritionInputs = foodNutritionData.map(entry => {
        const field = entry[0];
        const label = entry[1];

        const initialValue = item?.food ? (item?.food as any)[field] : 0;

        return <div className="row g-3 align-items-center mt-2">
            <div className="col-auto text-center">
                <label style={{width: "120px"}} className="col-form-label" htmlFor={field}>{label}</label>
            </div>
            <div className="col-auto">
                <input className="form-control" id={field} type="number" name={field} title={label}
                    defaultValue={initialValue} />
            </div>
        </div>;
    });

    return (
        <>
            <h3>Food stock 🥫</h3>
            <div className="d-flex column-gap-3 mt-2">
                <div className="flex-grow-1">
                    <label className="form-label">Servings per item amount:</label>
                    <input className="form-control" type="number"></input>
                </div>
                <div className="flex-grow-1">
                    <label className="form-label" htmlFor="servings">Servings:</label>
                    <input className="form-control" id="servings" type="number" name="servings" title="Food servings:"
                        required />
                </div>
            </div>

            <div className="mt-2">
                <label className="form-label">Serving size:</label>
                <QuantityInput title="Serving size:" name="servingSize" initialQuantity={null} required={true} />
            </div>

            <h3 className="mt-4">Nutrition per item 🍑</h3>

            {nutritionInputs}

            
        </> 
    );
}
