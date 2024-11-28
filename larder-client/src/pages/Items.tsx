import { useEffect, useState } from "react";
import { ItemDto } from "../types/Item";
import { apiClient } from "../util/axios";
import ItemsTable from "../components/tables/ItemsTable";
import { Link } from "react-router-dom";
import { ItemSortOptions } from "../types/ItemSortOptions";

export default function Items() {
    const [items, setItems] = useState<ItemDto[]>([]); 
    const [searchParam] = useState("");
    const [sortOrder, setSortOrder] = useState(ItemSortOptions.Name);

    useEffect(() => {
        apiClient.get<ItemDto[]>("/api/items", { params: {search: searchParam, sortOrder: sortOrder}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortOrder])

    return (
        <>
            <div className="page-flex-header">
                <h1>Items</h1>
                <Link className="btn btn-primary" to={"/items/new"}>New item</Link>
            </div>

            <ItemsTable items={items} sortOrder={sortOrder} setSortOrder={setSortOrder} />
        </>
    );
}
