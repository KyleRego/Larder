import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import { formatQuantity } from "../types/QuantityDto";
import QuantitySpan from "../components/QuantitySpan";
import NutritionCard from "../components/cards/NutritionCard";
import EditLink from "../components/EditLink";

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
    }, [])

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
                        {item.name}
                    </li>
                </ol>
            </nav>

            <div className="d-flex align-items-center column-gap-3">
                <h1>{`${item.name}`}</h1>

                <EditLink   path={`/items/${item.id}/edit`}
                            title={`Edit ${item.name}`} />
            </div>

            { item.quantity &&
            <h2 className="my-4">
                Quantity: <QuantitySpan quantity={item.quantity} />
            </h2>
            }

            {item.nutrition !== null &&
                <div>
                    <NutritionCard nutrition={item.nutrition} />

                    <div className="card">
                        <div className="card-body">
                            <h4 className="card-title">Eat quantity:</h4>
                            <p>Quantity available: {item.quantity !== null ? formatQuantity(item.quantity!) : "N/a"}</p>
                
                            <p>Serving size: <QuantitySpan quantity={item.nutrition!.servingSize} /></p>
                        </div>
                    </div>
                </div>
            }
        </>
    )
}
