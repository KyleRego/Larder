import { useEffect, useState } from "react";
import { Item } from "../types/Item";
import { apiClient } from "../util/axios";

export default function Items() {
    const [items, setItems] = useState<Item[]>([]); 
    const [searchParam] = useState("");
    const [sortByParam] = useState("");

    useEffect(() => {
        apiClient.get<Item[]>("/api/items", { params: {search: searchParam, sortBy: sortByParam}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortByParam])

    const itemsListElements = items.map(item => {
        return <li key={item.id}>{item.name}</li>;
    })

    return (
        <>
            <h1>Items</h1>

            <ol>
                {itemsListElements}
            </ol>
        </>
    );
}
