import { ItemDto } from "../types/Item";
import QuantityInput from "./QuantityInput";

export default function QuantityComponentFormControls({item} : {item: ItemDto | null}) {
    return (
        <>
            <div className="d-flex flex-wrap row-gap-3 column-gap-3">
                <div className="flex-grow-1">
                    <label>Quantity per item:</label>
                    <QuantityInput name="quantityPerItem" title="Quantity per item"
                        initialQuantity={item?.quantityComp?.quantity ?? null} />
                </div>

                <div className="flex-grow-1">
                    <label>Quantity:</label>
                    <QuantityInput name="quantity" title="Quantity"
                        initialQuantity={item?.quantityComp?.quantityPerItem ?? null} />
                </div>
            </div>
        </>
    );
}
