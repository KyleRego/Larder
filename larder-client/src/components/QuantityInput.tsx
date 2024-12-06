import { QuantityDto } from "../types/QuantityDto";
import UnitsSelect from "./UnitsSelect";

export default function QuantityInput({name, title, initialQuantity, required = false}
            : { name: string,
                title: string,
                initialQuantity: QuantityDto | null,
                required?: boolean}) {
    return (
        <>
            <div className="d-flex justify-content-center column-gap-3" title={title}>
                <div className="">
                    <input className="form-control" type="number" name={name} title={`${title} amount:`}
                        defaultValue={String(initialQuantity?.amount ?? 0)}
                        required = {required}></input>
                </div>

                <div className="flex-grow-1">
                    <UnitsSelect selectName={`${name}Unit`}
                            selectTitle = {`${title} unit:`}
                            defaultValue={initialQuantity?.unitId ?? null} />
                </div>
            </div>  
        </>
    );
}
