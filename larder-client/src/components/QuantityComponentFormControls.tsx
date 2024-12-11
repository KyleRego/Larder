import { Dispatch, SetStateAction } from "react";
import { ItemDto } from "../types/ItemDto";
import QuantityInput from "./QuantityInput";
import { QuantityComponentDto } from "../types/QuantityComponentDto";
import { QuantityDto } from "../types/QuantityDto";

export default function QuantityComponentFormControls({item, setItem}
    : { item: ItemDto | null,
        setItem: Dispatch<SetStateAction<ItemDto>> }) {
    
    function updateItemQuantity<K extends keyof QuantityComponentDto,
                                Q extends keyof QuantityDto>
                    (field: K, quantityField: Q, value: QuantityDto[Q]) {
        setItem((prevItem) => {
            return { ...prevItem, quantityComp: {
                    ...prevItem.quantityComp, [field]: {
                        ...prevItem.quantityComp && prevItem.quantityComp[field],
                        [quantityField]: value
                    }
                }
            } as ItemDto;
        })
    }

    return (
        <>
            <div className="d-flex flex-wrap row-gap-3 column-gap-3">
                <div className="flex-grow-1">
                    <label className="form-label">Quantity per item:</label>
                    <QuantityInput name="quantityPerItem" title="Quantity per item"
                        quantity={item?.quantityComp?.quantityPerItem ?? null}
                        onChangeAmount={(e) => updateItemQuantity("quantityPerItem", "amount", parseFloat(e.target.value))}
                        onChangeUnit={(e) => updateItemQuantity("quantityPerItem", "unitId", e.target.value)} />
                </div>

                <div className="flex-grow-1">
                    <label className="form-label">Quantity:</label>
                    <QuantityInput name="quantity" title="Quantity"
                        quantity={item?.quantityComp?.quantity ?? null}
                        onChangeAmount={(e) => updateItemQuantity("quantity", "amount", parseFloat(e.target.value))}
                        onChangeUnit={(e) => updateItemQuantity("quantity", "unitId", e.target.value)} />
                </div>
            </div>
        </>
    );
}
