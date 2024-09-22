import { TiArrowUnsorted } from "react-icons/ti";
import { TiArrowSortedUp } from "react-icons/ti";
import { TiArrowSortedDown } from "react-icons/ti";

// Convention: columnName and columnName_Desc are supported sortBy query strings 
export default function SortingTableHeader({columnName, sortOrder, setSortOrder,
                                                columnDisplayText=null})
{
    const sortDesc = `${columnName}_Desc`;

    function handleOnClick()
    {
        sortOrder === columnName ? setSortOrder(sortDesc) : setSortOrder(columnName);
    }

    let icon;

    if (sortOrder === columnName)
    {
        icon = <TiArrowSortedUp className="w-5 h-5" />;
    }
    else if (sortOrder === sortDesc)
    {
        icon = <TiArrowSortedDown className="w-5 h-5" />;
    }
    else
    {
        icon = <TiArrowUnsorted className="w-5 h-5" />;
    }

    return (
        <th title="Change ordering" className="" role="button" onClick={handleOnClick} scope="col">
            <div className="d-flex justify-content-center column-gap-1 flex-wrap column-gap-1 align-items-center">
                <span className="">{columnDisplayText ?? columnName}</span>
                <span className="flex-grow-1">{icon}</span>
            </div>
        </th>
    );
}
