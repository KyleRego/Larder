import { useState } from "react";

import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";
import QuantityInput from "./QuantityInput";

export default function EditableQuantityTableCell({quantity, handleSubmit, units})
{
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
                                    setEditing={setEditing} />
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
            <div className="d-flex column-gap-3 align-items-center m-0">
                <QuantityInput initialQuantity={quantity} units={units} />
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

function NoneditingTableCell({quantity, setEditing})
{
    function startEditing()
    {
        setEditing(true);
    }

    return <td className="py-0">
        <div className="m-0 d-flex column-gap-3 align-items-center">
            <span>{quantity?.amount} {quantity?.unitName}</span>
            <CiEdit className="w-5 h-5 cursor-pointer" onClick={startEditing} title="Edit quantity" />
        </div>
    </td>
}
