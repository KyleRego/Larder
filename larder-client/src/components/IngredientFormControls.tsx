import { ItemDto } from "../types/Item";
import QuantityInput from "./QuantityInput";

export default function QuantityComponentFormControls({item} : {item: ItemDto | null}) {
    return (
        <>
            <div className="d-flex column-gap-3">
                <div className="flex-grow-1">
                    <label>Quantity per item amount:</label>
                    <QuantityInput item={item} />
                </div>

                <div className="flex-grow-1">
                    <label>Quantity:</label>
                    <QuantityInput item={item} />
                </div>
            </div>
        </>
    );
}
