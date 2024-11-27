import { useEffect, useState } from "react";
import { ItemDto } from "../types/Item";
import { apiClient } from "../util/axios";
import ItemsTable from "../components/ItemsTable";
import { Link } from "react-router-dom";

export default function Items() {
    const [items, setItems] = useState<ItemDto[]>([]); 
    const [searchParam] = useState("");
    const [sortByParam] = useState("");

    useEffect(() => {
        apiClient.get<ItemDto[]>("/api/items", { params: {search: searchParam, sortBy: sortByParam}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortByParam])

    return (
        <>
            <div className="page-flex-header">
                <h1>Items</h1>
                <Link className="btn btn-primary" to={"/items/new"}>New item</Link>
            </div>

            <ItemsTable items={items} />
        </>
    );
}
