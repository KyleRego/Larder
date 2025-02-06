import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import NutritionCard from "../components/cards/NutritionCard";
import ItemCard from "../components/cards/ItemCard";
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
                        <h1 className="d-inline fs-6">{item.name}</h1>
                    </li>
                </ol>
            </nav>

            <div className="d-flex align-items-start column-gap-3 row-gap-3 flex-wrap my-4">
            
                <div className="d-flex flex-row flex-md-column column-gap-3 align-items-center row-gap-3 flex-wrap">
                    <ItemCard item={item} />
                    <EditLink   path={`/items/${item.id}/edit`}
                                                        title={`Edit ${item.name}`} />
                </div>
                
                {item.nutrition &&
                    <div className="d-flex flex-row flex-md-column column-gap-3 align-items-center row-gap-3 flex-wrap">
                        <NutritionCard nutrition={item.nutrition} />
                        <Link to={`/items/${item.id}/eat`} title={`Eat ${item.name}`} className="btn btn-dark">
                            Eat food
                        </Link>
                    </div>
                }
            </div>
        </>
    )
}
