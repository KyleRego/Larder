import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import { formatQuantity } from "../types/QuantityDto";
import FoodNutritionTable from "../components/tables/FoodNutritionTable";
import QuantitySpan from "../components/QuantitySpan";

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
                    <li className="breadcrumb-item active" aria-current="page">
                        <Link to={"/items"}>Back to items</Link>
                    </li>
                </ol>
            </nav>

            <div className="d-flex justify-content-around align-items-center">
                <h1>{`${item.name}`}</h1>

                { item.quantity &&
                <h2>
                    <QuantitySpan quantity={item.quantity} />
                </h2>
                }
            </div>

            {item.food !== null &&
                <div className="d-flex flex-wrap column-gap-3 row-gap-3 align-items-top justify-content-around p-4">
                    <div className="card">
                        <div className="card-body">
                            <h3 className="card-title">Nutrition</h3>

                            <FoodNutritionTable food={item.food!} />
                        </div>
                    </div>

                    <div className="card">
                        <div className="card-body">
                            <h4 className="card-title">Eat servings:</h4>
                            <p className="mt-4">Servings available: {String(item.food!.servings)}</p>
                            <div>
                                <form>
                                    <div>
                                        <label className="form-label">Servings to eat:</label>
                                        <input type="number"></input>
                                    </div>
                                    <div className="mt-2">
                                        <button className="btn btn-primary" type="submit">Submit</button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>

                    <div className="card">
                        <div className="card-body">
                            <h4 className="card-title">Eat quantity:</h4>
                            <p>Quantity available: {item.quantity !== null ? formatQuantity(item.quantity!) : "N/a"}</p>
                
                            <p>Serving size: {formatQuantity(item.food!.servingSize)}</p>
                        </div>
                    </div>
                </div>
            }
        </>
    )
}
