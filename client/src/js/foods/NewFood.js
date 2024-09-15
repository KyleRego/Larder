import { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import FoodForm from "./FoodForm";
import FoodsService from "../services/FoodsService";
import FoodFormDataMapper from "./FoodFormDataMapper";
import { AlertContext } from "../../AlertContext";
import { UnitsContext } from "../../UnitsContext";

export default function NewFood() {
    const { units } = useContext(UnitsContext)
    const { setAlertMessage } = useContext(AlertContext)
    const navigate = useNavigate();

    async function handleSubmit(e) {
        e.preventDefault();

        const formData = new FormData(e.target);
        const food = FoodFormDataMapper.map(formData);
        const service = new FoodsService();

        service.postFood(food).then(() => {
            setAlertMessage(`Food "${food.name}" was created.`);
            navigate("/foods");
        }).catch((error) => {
            setAlertMessage(`Something went wrong creating the food: ${error.message}`);
        })
    }

    return <>
        <h1>New food:</h1>

        <FoodForm initialFood={{amount: 0}} units={units} handleSubmit={handleSubmit} />

        <Link to={"/foods"}>Back to foods</Link>
    </>
}
