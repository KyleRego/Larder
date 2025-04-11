import { Link, useNavigate } from "react-router-dom";
import ItemForm from "../forms/ItemForm";
import { ItemDto } from "../../types/dtos/ItemDto";
import { useApiRequest } from "../../hooks/useApiRequest";
import ActionBar from "../layout/ActionBar";
import BreadCrumbs from "../layout/Breadcrumbs";

export default function NewItem() {
    const { handleRequest } = useApiRequest();
    const navigate = useNavigate();

    function initialNewItem() : ItemDto {
        return {
            id: null,
            name: "",
            description: "",
            nutrition: null,
            quantity: { amount: 1, unitId: null, unitName: null },
            consumedTime: null
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

    return <div className="h-100 d-flex flex-column">
        <BreadCrumbs>
            <li className="breadcrumb-item" aria-current="page">
                <Link to={"/items"}>Items</Link>
            </li>
            <li className="breadcrumb-item active">
                <h1 className="fs-6 d-inline">
                    New item
                </h1>
            </li>
        </BreadCrumbs>
        
        <div className="my-2 container flex-grow-1">
            <ItemForm initialItem={initialNewItem()} submitFormItem={submitFormItem} />
        </div>

        <ActionBar>
            <div className="d-flex justify-content-center">
                <button type="submit" form="item-form"
                        className="btn btn-outline-light"
                        id="item-form-submit">
                    Create item
                </button>
            </div>
        </ActionBar>  
    </div>;
}
