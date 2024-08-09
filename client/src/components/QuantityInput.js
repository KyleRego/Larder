import UnitSelectOptions from "../units/UnitSelectOptions";

export default function QuantityInput({initialAmount, initialUnitId, units})
{
    return <>
        <label htmlFor="amount">Quantity:</label>
        <input id="amount" name="amount" type="number" defaultValue={initialAmount}></input>
        <label hidden htmlFor="unit">Unit:</label>
        <select id="unit" title="unit" name="unitId" defaultValue={initialUnitId}>
            <UnitSelectOptions units={units} />
        </select>
    </>
}
