import { ItemDto } from "../types/Item";
import UnitsSelect from "./UnitsSelect";

export default function QuantityInput({item}
                            : {item: ItemDto | null}) {
    return (
        <>
            <div className="d-flex column-gap-3">
                <div className="flex-grow-1">
                    <input className="form-control" type="number"
                        defaultValue={String(item?.ingredient?.quantity?.amount) ?? null}></input>
                </div>

                <div>
                    <UnitsSelect selectName="quantityUnit"
                            selectTitle="Quantity unit:"
                            defaultValue={item?.ingredient?.quantity?.unit?.id ?? null} />
                </div>
            </div>  
        </>
    );
}
