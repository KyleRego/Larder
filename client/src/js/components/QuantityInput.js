import UnitSelectOptions from "../units/UnitSelectOptions";

export default function QuantityInput({quantity, units}) {
    const amountInputId = "amount";
    const unitInputId = "unitId";
    const labelText = "Quantity:";

    return <div className="d-flex flex-wrap align-items-center column-gap-1 row-gap-1">
        <label htmlFor={amountInputId}>{labelText}</label>
        <input id={amountInputId} name={amountInputId} type="number" defaultValue={quantity?.amount}></input>
        <label hidden htmlFor={unitInputId}></label>
        <select id={unitInputId} title="unit" name={unitInputId} defaultValue={quantity?.unitId}>
            <UnitSelectOptions units={units} />
        </select>
    </div>;
}
