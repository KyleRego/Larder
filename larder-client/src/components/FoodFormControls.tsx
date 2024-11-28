import { ItemDto } from "../types/Item";
import QuantityInput from "./QuantityInput";

export default function FoodFormControls({item} : {item: ItemDto | null}) {
    return (
        <>
            <div className="d-flex column-gap-3 mt-2">
                <div className="flex-grow-1">
                    <label>Serving size:</label>
                    <QuantityInput title="Serving size:" name="servingSize" item={item} />
                </div>

                <div className="flex-grow-1">
                    <label htmlFor="caloriesInput">Calories per serving:</label>
                    <input className="form-control" id="caloriesInput" type="number" name="calories" title="Calories per serving:" />
                </div>  
            </div>

            <div className="d-flex column-gap-3 mt-2">
                <div className="flex-grow-1">
                    <label>Servings per item amount:</label>
                    <input className="form-control" type="number"></input>
                </div>
                <div className="flex-grow-1">
                    <label htmlFor="servings">Servings:</label>
                    <input className="form-control" id="servings" type="number" name="servings" title="Food servings:" />
                </div>
            </div>

            
        </> 
    );
}
