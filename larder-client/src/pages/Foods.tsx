import { useEffect, useState } from "react";
import { ItemDto } from "../types/ItemDto";
import { apiClient } from "../util/axios";
import FoodsTable from "../components/tables/FoodsTable";
import { FoodSortOptions } from "../types/FoodSortOptions";

export default function Foods() {
    const [items, setItems] = useState<ItemDto[]>([]); 
    const [searchParam] = useState("");
    const [sortOrder, setSortOrder] = useState(FoodSortOptions.Name);

    useEffect(() => {
        apiClient.get<ItemDto[]>("/api/foods", { params: {search: searchParam, sortBy: sortOrder}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortOrder])

    return (
        <>
            <div className="page-flex-header">
                <h1>Foods</h1>
            </div>

            <div className="mt-4">
                <FoodsTable items={items} sortOrder={sortOrder} setSortOrder={setSortOrder} />
            </div>    
        </>
    );
}
