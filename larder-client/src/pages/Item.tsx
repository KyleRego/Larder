import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import { formatQuantity } from "../types/QuantityDto";
import QuantitySpan from "../components/QuantitySpan";
import NutritionCard from "../components/cards/NutritionCard";
import ItemCard from "../components/cards/ItemCard";

export default function Item() {
    const { id } = useParams<{id: string}>();
    const [item, setItem] = useState<ItemDto | null>(null);
    const { handleRequest } = useApiRequest();

    useEffect(() => {
        async function getItem() {
            const res = await handleRequest<ItemDto>({
                method: "get",
                url: `/api/items/${id}`
            });

            if (res) {
                setItem(res);
            }
        }

        getItem();
    }, [id])

    if (item === null) {
        return <Loading />
    }

    return (
        <>
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item" aria-current="page">
                        <Link to={"/items"}>Items</Link>
                    </li>
                    <li className="breadcrumb-item active">
                        <h1 className="fs-6">{item.name}</h1>
                    </li>
                </ol>
            </nav>

            <div className=" d-sm-flex-row flex-column align-items-sm-start align-items-end my-4 d-flex column-gap-3 row-gap-3 flex-wrap">
            { item.quantity &&
                <ItemCard item={item} />
            }

            {item.nutrition &&
                <NutritionCard nutrition={item.nutrition} />
            }

            {item.nutrition &&
                <div className="card">
                        <div className="card-body">
                            <h4 className="card-title">Eat quantity:</h4>
                            <p>Quantity available: {item.quantity !== null ? formatQuantity(item.quantity!) : "N/a"}</p>
                
                            <p>Serving size: <QuantitySpan quantity={item.nutrition!.servingSize} /></p>
                        </div>
                    </div>
            }
            </div>
        </>
    )
}
