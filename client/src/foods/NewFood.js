import { Link } from "react-router-dom";

import FoodForm from "./FoodForm";

import FoodsService from "../services/FoodsService";

import FoodFormDataMapper from "./FoodFormDataMapper";

export default function NewFood({units})
{
    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const food = FoodFormDataMapper.map(formData);

        const service = new FoodsService();

        await service.postFood(food);
    }

    return <>
        <h1>New food:</h1>

        <FoodForm initialFood={{amount: 0}} units={units} handleSubmit={handleSubmit} />

        <Link to={"/foods"}>Back to foods</Link>
    </>
}