import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import NutritionCard from "../components/cards/NutritionCard";
import ItemCard from "../components/cards/ItemCard";
import BreadCrumbs from "../Breadcrumbs";
import ActionBar from "../ActionBar";

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
        <div className="d-flex h-100 flex-column">
            <BreadCrumbs>
                <li className="breadcrumb-item">
                    <Link to={"/items"}>Items</Link>
                </li>
                <li className="breadcrumb-item active">
                    <h1 className="d-inline fs-6">
                        {item.name}
                    </h1>
                </li>
            </BreadCrumbs>

            <div className="my-2 container flex-grow-1 d-flex align-items-start column-gap-3 row-gap-3 flex-wrap my-4">
            
                <div className="d-flex flex-row flex-md-column column-gap-3 align-items-center row-gap-3 flex-wrap">
                    <ItemCard item={item} />
                </div>
                
                {item.nutrition &&
                    <div className="d-flex flex-row flex-md-column column-gap-3 align-items-center row-gap-3 flex-wrap">
                        <NutritionCard nutrition={item.nutrition} />
                        
                    </div>
                }
            </div>

            <ActionBar>
                <div className="d-flex justify-content-center">
                    <div className="btn-group">
                            <Link   className="btn btn-outline-light"
                                    to={`/items/${item.id}/edit`}
                                    title={`Edit ${item.name}`} >
                                {`Edit ${item.name}`}
                            </Link>

                            {item.nutrition &&
                            <Link to={`/items/${item.id}/eat`} title={`Eat ${item.name}`}
                                    className="btn btn-outline-light">
                                Eat {`${item.name}`}
                            </Link>
                            }
                    </div>
                </div>
            </ActionBar>
        </div>
    )
}
