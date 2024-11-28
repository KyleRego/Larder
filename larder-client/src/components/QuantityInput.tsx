import { ItemDto } from "../types/Item";
import UnitsSelect from "./UnitsSelect";

export default function QuantityInput({name, title, item}
                            : {name: string, title: string, item: ItemDto | null}) {
    return (
        <>
            <div className="d-flex column-gap-3" title={title}>
                <div className="flex-grow-1">
                    <input className="form-control" type="number" name={name} title={`${title} amount:`}
                        defaultValue={String(item?.ingredient?.quantity?.amount) ?? null}></input>
                </div>

                <div>
                    <UnitsSelect selectName={`${name}Unit`}
                            selectTitle = {`${title} unit:`}
                            defaultValue={item?.ingredient?.quantity?.unit?.id ?? null} />
                </div>
            </div>  
        </>
    );
}
