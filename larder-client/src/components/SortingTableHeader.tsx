import { Dispatch, SetStateAction } from "react";
import { TiArrowUnsorted } from "react-icons/ti";
import { TiArrowSortedUp } from "react-icons/ti";
import { TiArrowSortedDown } from "react-icons/ti";

export default function SortingTableHeader<T>({ascending, descending, sortOrder,
                                                setSortOrder, headerText} : 
                {ascending: T, descending: T, sortOrder: T,
                    setSortOrder: Dispatch<SetStateAction<T>>, headerText: string}) {

        function handleClick() {
            sortOrder === ascending ? setSortOrder(descending) : setSortOrder(ascending);
        }

        let icon;
    
        if (ascending === sortOrder) {
            icon = <TiArrowSortedUp />
        } else if (descending === sortOrder) {
            icon = <TiArrowSortedDown />
        } else {
            icon = <TiArrowUnsorted />
        }

        return (
            <th title="Change ordering" role="button" onClick={handleClick} scope="col">
                <div className="d-flex justify-content-start column-gap-1 w-min-max-content">
                    <span className="">{headerText}</span>
                    <span className="mt-1 flex-grow-1 d-inline-flex align-items-center">{icon}</span>
                </div>
            </th>
        );

}