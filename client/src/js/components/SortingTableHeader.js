import { TiArrowUnsorted } from "react-icons/ti";
import { TiArrowSortedUp } from "react-icons/ti";
import { TiArrowSortedDown } from "react-icons/ti";

// Convention: columnName and columnName_Desc are supported sortBy query strings 
export default function SortingTableHeader({columnName, sortOrder, setSortOrder})
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
        <th title="Change ordering" className="cursor-pointer" onClick={handleOnClick} scope="col">
            <div className="d-flex align-items-center">
                <span className="">{columnName}</span>
                {icon}
            </div>
        </th>
    );
}
