import { Link, useNavigate } from "react-router-dom";
import ItemForm from "../forms/ItemForm";
import { ItemDto } from "../types/ItemDto";
import { useApiRequest } from "../hooks/useApiRequest";


export default function NewItem2() {
    const { handleRequest } = useApiRequest();
    const navigate = useNavigate();

    function initialNewItem() : ItemDto {
        return {
            id: null,
            name: "",
            description: "",
            nutrition: null,
            ingredient: null,
            quantity: { amount: 1, unitId: null, unitName: null },
        }
    }

    async function submitFormItem(item: ItemDto) {
        const res = await handleRequest<ItemDto>({
            method: "post",
            url: "/api/Items",
            data: item
        });

        if (res) {
            navigate("/items");
        }
    }

    return <div>

        <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
                <li className="breadcrumb-item" aria-current="page">
                    <Link to={"/items"}>Items</Link>
                </li>
                <li className="breadcrumb-item active">
                    New item
                </li>
            </ol>
        </nav>

        <div className="my-2">
            <ItemForm initialItem={initialNewItem()} submitFormItem={submitFormItem} />
        </div>

        <div>
            <button type="submit" form="item-form"
                    className="btn btn-outline-primary">
                Create item
            </button>
        </div>
        
    </div>;
}
