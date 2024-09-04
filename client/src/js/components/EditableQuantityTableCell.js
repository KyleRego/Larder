import { useState } from "react";

import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";
import QuantityInput from "./QuantityInput";

import findUnitName from "../helpers/findUnitName";

export default function EditableQuantityTableCell({quantity, handleSubmit, units})
{
    const unitName = findUnitName(quantity.unitId, units);

    const [editing, setEditing] = useState(false);

    if (editing === true)
    {
        return <EditingTableCell    quantity={quantity}
                                    setEditing={setEditing}
                                    handleSubmit={handleSubmit}
                                    units={units} />
    }
    else
    {
        return <NoneditingTableCell quantity={quantity}
                                    setEditing={setEditing}
                                    unitName={unitName} />
    }
}

function EditingTableCell({quantity, setEditing, handleSubmit, units})
{
    async function innerHandleSubmit(e)
    {
        await handleSubmit(e);
        setEditing(false);
    }

    function cancelEditing()
    {
        setEditing(false);
    }

    return <td className="py-0">
        <form onSubmit={innerHandleSubmit}>
            <div className="d-flex flex-wrap column-gap-1 row-gap-1 py-2 align-items-center m-0">
                <QuantityInput quantity={quantity} units={units} />
                <button className="btn btn-primary btn-sm" type="submit" title="Done">
                    <MdDone />
                </button>

                <button className="btn btn-danger btn-sm" type="button" onClick={cancelEditing} title="Cancel">
                    Cancel
                </button>
            </div>
        </form>
    </td>
}

function NoneditingTableCell({quantity, setEditing, unitName})
{
    function startEditing()
    {
        setEditing(true);
    }

    return <td className="py-0">
        <div className="m-0 d-flex column-gap-1 align-items-center">
            <span>{quantity?.amount} {unitName}</span>
            <CiEdit className="w-5 h-5" role="button" onClick={startEditing} title="Edit quantity" />
        </div>
    </td>
}
