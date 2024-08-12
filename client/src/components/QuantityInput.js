import UnitSelectOptions from "../units/UnitSelectOptions";

export default function QuantityInput({initialQuantity, units, name = null})
{
    function formatString(str)
    {
        let capitalized = str[0].toUpperCase() + str.slice(1);

        return capitalized;
    }

    const amountInputId = (name) ? `${name}_amount` : "amount";
    const unitInputId = (name) ? `${name}_unitId` : "unitId";
    const labelText = (name) ? `${formatString(name)}:` : "Quantity:";

    return <>
        <label htmlFor={amountInputId}>{labelText}</label>
        <input className="ms-1" id={amountInputId} name={amountInputId} type="number" defaultValue={initialQuantity?.amount}></input>
        <label hidden htmlFor={unitInputId}></label>
        <select className="ms-1" id={unitInputId} title="unit" name={unitInputId} defaultValue={initialQuantity?.unitId}>
            <UnitSelectOptions units={units} />
        </select>
    </>

}
