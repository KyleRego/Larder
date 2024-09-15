import { useState, useEffect, useContext } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import FoodForm from "./FoodForm";
import FoodsService from "../services/FoodsService";

import FoodFormDataMapper from "./FoodFormDataMapper";
import { AlertContext } from "../../AlertContext";

export default function EditFood()
{
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext)

    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const food = FoodFormDataMapper.map(formData);
        food.id = id;

        const service = new FoodsService();

        await service.putFood(food).then(() => {
            setAlertMessage(`Food "${food.name}" was updated.`);
            navigate(`/foods/${food.id}`);
        }).catch(error => {
            console.log(error);
        });
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
        <h1>Editing food: {food.name}</h1>

        <div>
            <FoodForm initialFood={food} units={units} handleSubmit={handleSubmit} />
        </div>

        <div>
            <Link to={`/foods/${id}`}>Back to food</Link>
        </div>
    </>
}
