import { QuantityDto } from "../types/QuantityDto";
import UnitsSelect from "./UnitsSelect";

export default function QuantityInput({name, title, quantity, required = false,
                                    onChangeAmount = () => {}, onChangeUnit = () => {}}
            : { name: string,
                title: string,
                quantity: QuantityDto | null,
                required?: boolean,
                onChangeAmount?: (e: React.ChangeEvent<HTMLInputElement>) => void,
                onChangeUnit?: (e: React.ChangeEvent<HTMLSelectElement>) => void}) {
    return (
        <>
            <div className="d-flex justify-content-center column-gap-3" title={title}>
                <div>
                    <input  name={name} title={`${title} amount:`}
                            className="form-control" type="number"
                            value={String(quantity?.amount ?? 0)}
                            required={required}
                            onChange={onChangeAmount} />
                </div>

                <div className="flex-grow-1">
                    <UnitsSelect selectName={`${name}Unit`}
                            selectTitle={`${title} unit:`}
                            value={quantity?.unitId ?? null} onChange={onChangeUnit} />
                </div>
            </div>
        </>
    );
}
