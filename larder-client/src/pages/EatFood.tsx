import { Link, useNavigate, useParams } from "react-router-dom";
import Loading from "../components/Loading";
import { ItemDto } from "../types/ItemDto";
import { useEffect, useState } from "react";
import { useApiRequest } from "../hooks/useApiRequest";
import QuantityInput from "../forms/QuantityInput";
import { QuantityDto } from "../types/QuantityDto";
import { EatFoodDto } from "../types/EatFoodDto";

export default function EatFood({}) {
    const { id } = useParams<{id: string}>();
    const [item, setItem] = useState<ItemDto | null>(null);
    const [eatFoodDto, setEatFoodDto] = useState<EatFoodDto>(
        {itemId: "", quantityEaten: { amount: 0, unitId: null, unitName: ""}});
    const { handleRequest } = useApiRequest();
    const navigate = useNavigate();

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

    function updateEatFoodDto(quantityEaten: QuantityDto) {
        setEatFoodDto({...eatFoodDto, quantityEaten: quantityEaten})
    }

    async function postEatFood(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const itemId: string = item?.id!
        setEatFoodDto({...eatFoodDto, itemId: itemId});

        const res: ItemDto | null = await handleRequest<ItemDto>({
            method: "post",
            url: `/api/Foods/EatFood/${itemId}`,
            data: eatFoodDto
        });

        if (res) {
            navigate(`/items/${itemId}`);
        }
    }

    return <>
        <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
                <li className="breadcrumb-item" aria-current="page">
                    <Link to={"/items"}>Items</Link>
                </li>
                <li className="breadcrumb-item">
                    <h1 className="d-inline fs-6">
                        <Link to={`/items/${item.id}`}>
                            {item.name}
                        </Link>
                    </h1>
                </li>
                <li className="breadcrumb-item">
                    Eating item
                </li>
            </ol>
        </nav>

        <div>
            <h2 className="my-2">Eat item quantity:</h2>

            <form id="eat-food-form" onSubmit={postEatFood}>
                <div className="my-2">
                    <QuantityInput quantityLabel="Quantity eaten"
                                initialQuantity={null}
                                handleQuantityChange={updateEatFoodDto} />
                </div>

                <button type="submit" className="btn btn-outline-primary">
                    Eat food
                </button>
            </form>
        </div>
    </>
}