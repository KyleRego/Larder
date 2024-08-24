import { Link, useOutletContext } from "react-router-dom";

import FoodForm from "./FoodForm";

import FoodsService from "../services/FoodsService";

import FoodFormDataMapper from "./FoodFormDataMapper";

export default function NewFood({units})
{
    const [setToastMessage, setShowToast] = useOutletContext();

    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const food = FoodFormDataMapper.map(formData);

        const service = new FoodsService();

        service.postFood(food).then(() => {
            setToastMessage("Food created");
            setShowToast(true);
        }).catch((error) => {
            setToastMessage(`Something went wrong: ${error}`);
            setShowToast(true);
        })
    }

    return <>
        <h1>New food:</h1>

        <FoodForm initialFood={{amount: 0}} units={units} handleSubmit={handleSubmit} />

        <Link to={"/foods"}>Back to foods</Link>
    </>
}
