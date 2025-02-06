import { useNavigate, useParams } from "react-router";
import ItemForm from "../forms/ItemForm";
import { useEffect, useState } from "react";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";
import Loading from "../components/Loading";
import { Link } from "react-router-dom";
import BreadCrumbs from "../Breadcrumbs";
import ActionBar from "../ActionBar";

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

    return <div className="h-100 d-flex flex-column">
        <BreadCrumbs>
            <li className="breadcrumb-item">
                    <Link to={"/items"}>Items</Link>
            </li>
            <li className="breadcrumb-item">
                <Link to={`/items/${item.id}`}>{item.name}</Link>
            </li>
            <li className="breadcrumb-item active">
                Editing item
            </li>
        </BreadCrumbs>

        <div className="container flex-grow-1">
            <ItemForm initialItem={item} submitFormItem={submitFormItem} />
        </div>

        <ActionBar>
            <div className="d-flex justify-content-center">
                <button type="submit" form="item-form"
                        className="btn btn-outline-light text-black border-black"
                        id="item-form-submit">
                    Update item
                </button>
            </div>
        </ActionBar>
    </div>;
}
