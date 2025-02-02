import { useNavigate, useParams } from "react-router";
import ItemForm from "../forms/ItemForm";
import { useEffect, useState } from "react";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import { Link } from "react-router-dom";

export default function EditItem() {
    const { handleRequest } = useApiRequest();
    const navigate = useNavigate();
    const { id } = useParams<{ id: string }>();
    const [item, setItem] = useState<ItemDto | null>(null)

    useEffect(() => {
        async function getItem() {
            const res: ItemDto | null = await handleRequest<ItemDto>({
                method: "get",
                url: `/api/Items/${id}`,
                data: item
            });

            if (res) {
                setItem(res);
            }
        }

        getItem();
    }, [id]);

    async function submitFormItem(item: ItemDto) {
        const res: ItemDto | null = await handleRequest<ItemDto>({
            method: "put",
            url: `/api/Items/${item.id}`,
            data: item
        });

        if (res) {
            navigate(`/items/${item.id}`);
        }
    }

    if (item === null) return <Loading />

    return <>
        <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
                <li className="breadcrumb-item">
                    <Link to={"/items"}>Items</Link>
                </li>
                <li className="breadcrumb-item">
                    <Link to={`/items/${item.id}`}>{item.name}</Link>
                </li>
                <li className="breadcrumb-item active">
                    Editing item
                </li>
            </ol>
        </nav>

        <div>
            <ItemForm initialItem={item} submitFormItem={submitFormItem} />
        </div>

        <div className="my-4">
            <button type="submit" form="item-form"
                    className="btn btn-outline-primary"
                    id="item-form-submit">
                Update item
            </button>
        </div>
    </>;
}
