import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { ItemDto } from "../types/Item";
import { useApiRequest } from "../hooks/useApiRequest";
import EditLink from "../components/EditLink";
import Loading from "../components/Loading";
import { formatQuantity } from "../types/QuantityDto";
import FoodNutritionTable from "../components/tables/FoodNutritionTable";

export default function Food() {
    const { id } = useParams<{id: string}>();
    const [item, setItem] = useState<ItemDto | null>(null);
    const { handleRequest } = useApiRequest();

    useEffect(() => {
        async function getFood() {
            const res = await handleRequest<ItemDto>({
                method: "get",
                url: `/api/foods/${id}`
            });

            if (res) {
                setItem(res);
            }
        }

        getFood();
    }, [])

    if (item === null) {
        return <Loading />
    }

    return (
        <>
            <div className="page-flex-header">
                <h1>{item.name}</h1>

                <EditLink path={`/items/${id}/edit`} title="Edit food item" />
            </div>

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
                        <p>Quantity available: {item.quantityComp !== null ? formatQuantity(item.quantityComp.quantity!) : "N/a"}</p>
            
                        <p>Serving size: {formatQuantity(item.food!.servingSize)}</p>
                    </div>
                </div>
            </div>

            <div className="mt-4">
                <Link className="btn btn-danger" to={"/foods"}>Back to foods</Link>
            </div>
        </>
    )
}
