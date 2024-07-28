import { useState } from "react";

import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";

export default function EditableQuantityTableCell({quantity, handleSubmit})
{
    const [editing, setEditing] = useState(false);

    if (editing === true)
    {
        return <EditingTableCell quantity={quantity} setEditing={setEditing} handleSubmit={handleSubmit} />
    }
    else
    {
        return <NoneditingTableCell quantity={quantity} setEditing={setEditing} />
    }
}

function EditingTableCell({quantity, setEditing, handleSubmit})
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

    return <td>
        <form onSubmit={innerHandleSubmit}>
            <div className="flex column-gap-5 align-items-center m-0">
                <label hidden htmlFor="number">Quantity</label>
                <input type="number" id="quantity" name="quantity" defaultValue={quantity} />
                <button type="submit">
                    <MdDone />
                </button>

                <button type="button" onClick={cancelEditing}>
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

    return <td>
        <div className="m-0 flex column-gap-5 align-items-center">
            {quantity}
            <CiEdit className="w-5 h-5" onClick={startEditing} />
        </div>
    </td>
}