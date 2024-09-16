import { useContext, useState } from "react";
import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";
import QuantityInput from "./QuantityInput";
import findUnitName from "../helpers/findUnitName";
import { UnitsContext } from "../../UnitsContext";

export default function EditableQuantityTableCell({quantity, handleSubmit}) {
    const { units } = useContext(UnitsContext);
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

function EditingTableCell({quantity, setEditing, handleSubmit, units}) {
    async function innerHandleSubmit(e)
    {
        await handleSubmit(e);
        setEditing(false);
    }

    return <td className="">
        <form onSubmit={innerHandleSubmit}>
            <div className="d-flex flex-wrap column-gap-5 row-gap-1 align-items-center m-0">
                <QuantityInput quantity={quantity} units={units} />
                <div className="d-flex flex-wrap align-items-center column-gap-3">
                    <button className="btn btn-primary btn-sm" type="submit" title="Done">
                        <MdDone className="w-5 h-5" />
                    </button>

                    <button className="btn btn-danger btn-sm" type="button" onClick={() => setEditing(false)} title="Cancel">
                        Cancel
                    </button>
                </div>
            </div>
        </form>
    </td>;
}

function NoneditingTableCell({quantity, setEditing, unitName}) {
    return <td className="">
        <div className="m-0 d-flex column-gap-1 align-items-center">
            <span>{quantity?.amount} {unitName}</span>
            <div className="btn btn-sm" role="button" onClick={() => setEditing(true)}>
                <CiEdit className="w-5 h-5" title="Edit quantity" />
            </div>
        </div>
    </td>;
}
