import { useState } from "react";

import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";
import QuantityInput from "./QuantityInput";

export default function EditableQuantityTableCell({amount, unitId, unitName, handleSubmit, units})
{
    const [editing, setEditing] = useState(false);

    if (editing === true)
    {
        return <EditingTableCell    amount={amount}
                                    unitId={unitId}
                                    setEditing={setEditing}
                                    handleSubmit={handleSubmit}
                                    units={units} />
    }
    else
    {
        return <NoneditingTableCell amount={amount}
                                    unitName={unitName}
                                    setEditing={setEditing} />
    }
}

function EditingTableCell({amount, unitId, setEditing, handleSubmit, units})
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
            <div className="flex column-gap-3 align-items-center m-0">
                <QuantityInput initialAmount={amount} initialUnitId={unitId} units={units} />
                <button type="submit" title="Done">
                    <MdDone />
                </button>

                <button type="button" onClick={cancelEditing} title="Cancel">
                    Cancel
                </button>
            </div>
        </form>
    </td>
}

function NoneditingTableCell({amount, unitName, setEditing})
{
    function startEditing()
    {
        setEditing(true);
    }
    debugger;

    return <td className="py-0">
        <div className="m-0 flex column-gap-3 align-items-center">
            <span>{amount} {unitName}</span>
            <CiEdit className="w-5 h-5 cursor-pointer" onClick={startEditing} title="Edit quantity" />
        </div>
    </td>
}
