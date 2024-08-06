import { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";

import FoodForm from "./FoodForm";
import FoodsService from "../services/FoodsService";

import FoodFormDataMapper from "./FoodFormDataMapper";

export default function EditFood()
{
    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const food = FoodFormDataMapper.map(formData);
        food.id = id;

        const service = new FoodsService();
        debugger;
        await service.putFood(food);
    }

    const { id } = useParams();
    const [food, setFood] = useState(null);

    useEffect(() => {
        const service = new FoodsService();

        service.getFood(id).then(result => {
            setFood(result);
        });

    }, [id]);

    if (food == null) return <h1>Loading...</h1>;

    return <>
        <h1>Editing food</h1>

        <div>
            <FoodForm initialFood={food} handleSubmit={handleSubmit} />
        </div>

        <div>
            <Link to={`/foods/${id}`}>Back to food</Link>
        </div>
    </>
}