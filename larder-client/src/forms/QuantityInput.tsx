import { useState } from "react";
import { QuantityDto } from "../types/QuantityDto";
import UnitsSelect from "../components/UnitsSelect";

export default function QuantityInput(
        { quantityLabel, initialQuantity, handleQuantityChange }
                                : { quantityLabel: string,
                        initialQuantity: QuantityDto | null,
        handleQuantityChange: (quantity: QuantityDto) => void }) {

    const [quantity, setQuantity] = useState<QuantityDto>(
                initialQuantity ?? { amount: 0, unitId: null, unitName: ""})
    const [withUnit, setWithUnit] = useState<boolean>(false);

    function handleAmountChange(e: React.ChangeEvent<HTMLInputElement>) {
        const newQuantity = {...quantity, amount: parseFloat(e.currentTarget.value)}
        setQuantity(newQuantity);
        handleQuantityChange(newQuantity);
    }

    function handleUnitSelect(e: React.ChangeEvent<HTMLSelectElement>) {
        const newQuantity = {...quantity, unitId: e.currentTarget.value};
        setQuantity(newQuantity);
        handleQuantityChange(newQuantity);
    }

    function removeUnit() {
        const newQuantity = {...quantity, unitId: null};
        setQuantity(newQuantity);
        handleQuantityChange(newQuantity);
        setWithUnit(false);
    }

    return (
        <>
            <div className="d-flex flex-wrap row-gap-3 column-gap-3">
                <div className="flex-grow-1">
                    <div className="d-flex align-items-center column-gap-3" title={quantityLabel}>
                    <div className="input-group">
                        <span className="input-group-text">{quantityLabel}</span>
                        <input  title={`${quantityLabel} amount`}
                                className="form-control" type="number"
                                value={String(quantity?.amount ?? 0)}
                                onChange={handleAmountChange}
                                id={`${quantityLabel}-amount-input`} />
                        { withUnit === false &&
                        <span role="button" title="+ unit"
                            className="input-group-text"
                            onClick={() => setWithUnit(true)}>
                            + unit
                        </span>
                        }

                        { withUnit === true &&
                        <div className="ms-2">
                            <div className="input-group">
                                <UnitsSelect selectName={"Unit"}
                                    selectTitle={`Serving size unit`}
                                    value={quantity?.unitId ?? null}
                                    onChange={handleUnitSelect} />
                                <span className="input-group-text" title="- unit"
                                        role="button"
                                        onClick={removeUnit}>
                                    - unit
                                </span>
                            </div>
                        </div>
                        }
                    </div>

                    
                </div>
                </div>
            </div>
        </>
    );
}
