import { ReactNode, useEffect, useState } from "react";
import { ItemDto } from "../../types/ItemDto";
import { ItemSortOptions } from "../../types/ItemSortOptions";
import SortingTableHeader from "../SortingTableHeader";
import { apiClient } from "../../util/axios";
import { useNavigate } from "react-router";
import QuantitySpan from "../QuantitySpan";

export default function ItemsTable({searchParam} : {searchParam: string }) {
    const [items, setItems] = useState<ItemDto[]>([]);         
    const [sortOrder, setSortOrder] = useState(ItemSortOptions.Name);

    const itemRows = items.map(item => {
        return <ItemRow key={item.id} item={item} />
    })

    useEffect(() => {
        apiClient.get<ItemDto[]>("/api/items", { params: {search: searchParam, sortOrder: sortOrder}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortOrder])
    
    return (
        <table className="table table-striped table-hover">
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
                                            headerText="Quantity" />
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
    const navigate = useNavigate();

    function handleRowClick() {
        navigate(`/items/${item.id}`);
    }

    return (
        <tr id={item.id!} onClick={handleRowClick} role="button">
            <th scope="row">{item.name}</th>
            <td>
                { item.quantity ?
                    <QuantitySpan quantity={item.quantity} />
                    : "N/a"
                }
            </td>
            <td>{item.description}</td>
        </tr>
    );
}
