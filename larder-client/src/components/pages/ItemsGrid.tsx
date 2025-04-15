import { useEffect, useState } from "react";
import { ItemDto } from "../../types/dtos/ItemDto";
import { apiClient } from "../../util/axios";
import ItemCard from "../cards/ItemCard";
import Loading from "../Loading";

export default function ItemsGrid() {
    const [items, setItems] = useState<ItemDto[] | null>(null);

    useEffect(() => {
            apiClient.get<ItemDto[]>("/api/items",
                { params: {}})
                .then(res => setItems(res.data))
                .catch(error => console.log(error));
        }, [])

    const itemCards = (items !== null) ? items.map(iteme => <ItemCard item={iteme} />) : <Loading />;

    return <div className="d-flex flex-wrap column-gap-3 row-gap-3 container p-4">
        {itemCards}
    </div>;
}
