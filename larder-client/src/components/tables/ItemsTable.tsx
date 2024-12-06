import { Dispatch, ReactNode, SetStateAction } from "react";
import { ItemDto } from "../../types/ItemDto";
import { ItemSortOptions } from "../../types/ItemSortOptions";
import SortingTableHeader from "../SortingTableHeader";

export default function ItemsTable({items, sortOrder, setSortOrder}
                : {items: ItemDto[]
                sortOrder: ItemSortOptions,
                setSortOrder: Dispatch<SetStateAction<ItemSortOptions>> }) {
    const itemRows = items.map(item => {
        return <ItemRow key={item.id} item={item} />
    })
    
    return (
        <table className="table table-striped text-break">
            <caption>
                Your items
            </caption>
            <thead>
                <tr>
                    <SortingTableHeader<ItemSortOptions> ascending={ItemSortOptions.Name}
                                            descending={ItemSortOptions.Name_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Name" />
                    <SortingTableHeader<ItemSortOptions> ascending={ItemSortOptions.Amount}
                                            descending={ItemSortOptions.Amount_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Amount" />
                    <SortingTableHeader<ItemSortOptions> ascending={ItemSortOptions.Description}
                                            descending={ItemSortOptions.Description_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Description" />
                </tr>
            </thead>
            <tbody>
                {itemRows}
            </tbody>
        </table>
    );
}

function ItemRow({item} : {item: ItemDto}) : ReactNode {
    return (
        <tr id={item.id!}>
            <th scope="row">{item.name}</th>
            <td>{item.amount}</td>
            <td>{item.description}</td>
        </tr>
    );
}
