import { useState } from "react";
import { QuantityDto } from "../types/QuantityDto";
import UnitsSelect from "./UnitsSelect";

export default function QuantityInput({quantity, handleAmountChange, handleUnitChange}
    : { quantity: QuantityDto | null,
        handleAmountChange:  (e: React.ChangeEvent<HTMLInputElement>) => void,
        handleUnitChange:  (e: React.ChangeEvent<HTMLSelectElement>) => void }) {

    const [withUnit, setWithUnit] = useState<boolean>(false);

    return (
        <>
            <div className="d-flex flex-wrap row-gap-3 column-gap-3">
                <div className="flex-grow-1">
                    <div className="d-flex align-items-center column-gap-3" title="Quantity:">
                    <div>
                        <input  title={`Amount:`}
                                className="form-control" type="number"
                                value={String(quantity?.amount ?? 0)}
                                onChange={handleAmountChange} />
                    </div>

                    { withUnit === false &&
                    <div>
                        <button type="button"
                            className="btn btn-outline-primary"
                            onClick={() => setWithUnit(true)}>
                            + unit
                        </button>
                    </div>
                    }

                    { withUnit === true &&
                    <div className="">
                        <UnitsSelect selectName={`${name}Unit`}
                                selectTitle={`Unit:`}
                                value={quantity?.unitId ?? null}
                                onChange={handleUnitChange} />
                    </div>
                    }
                </div>
                </div>
            </div>
        </>
    );
}
