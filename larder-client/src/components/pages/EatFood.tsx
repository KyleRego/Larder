import { Link, useNavigate, useParams } from "react-router-dom";
import Loading from "../Loading";
import { ItemDto } from "../../types/dtos/ItemDto";
import { useEffect, useState } from "react";
import { useApiRequest } from "../../hooks/useApiRequest";
import QuantityInput from "../forms/QuantityInput";
import { QuantityDto } from "../../types/dtos/QuantityDto";
import { EatFoodDto } from "../../types/dtos/EatFoodDto";
import BreadCrumbs from "../layout/Breadcrumbs";
import ActionBar from "../layout/ActionBar";

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
        const newEatFoodDto = {...eatFoodDto, quantityEaten: quantityEaten};
        setEatFoodDto(newEatFoodDto);
    }

    async function postEatFood(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const itemId: string = id!;
        const newEatFoodDto = {...eatFoodDto, itemId: itemId}
        setEatFoodDto(newEatFoodDto);

        const res: ItemDto | null = await handleRequest<ItemDto>({
            method: "post",
            url: `/api/Foods/EatFood/${itemId}`,
            data: newEatFoodDto
        });

        if (res) {
            navigate(`/items/${itemId}`);
        }
    }

    return <div className="d-flex flex-column h-100">
        <BreadCrumbs>
            <li className="breadcrumb-item" aria-current="page">
                <Link to={"/items"}>Items</Link>
            </li>
            <li className="breadcrumb-item">
                <Link to={`/items/${item.id}`}>
                    {item.name}
                </Link>
            </li>
            <li className="breadcrumb-item active">
                <h1 className="d-inline fs-6">
                    Eating food
                </h1>
            </li>
        </BreadCrumbs>

        <div className="container flex-grow-1">
            <h2 className="my-4">Eat item quantity:</h2>

            <form id="eat-food-form" onSubmit={postEatFood}>
                <div className="my-2">
                    <QuantityInput quantityLabel="Quantity eaten"
                                initialQuantity={null}
                                handleQuantityChange={updateEatFoodDto} />
                </div>
            </form>
        </div>

        <ActionBar>
            <div className="d-flex justify-content-center">
                <button type="submit" form="eat-food-form"
                        className="btn btn-outline-light border-black text-black">
                    Eat food
                </button>
            </div>
        </ActionBar>
    </div>
}