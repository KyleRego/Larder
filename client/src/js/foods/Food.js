import { useState, useEffect } from "react";
import { useParams, Link, useOutletContext } from "react-router-dom";

import EatFoodForm from "./EatFoodForm";

import FoodsService from "../services/FoodsService";

export default function Food()
{
    const [setToastMessage, setShowToast] = useOutletContext();

    let { id } = useParams();
    const [food, setFood] = useState(null);

    useEffect(() =>
    {
        const service = new FoodsService();

        service.getFood(id).then(result => {
            setFood(result);
        }).catch(error => {
            console.log(error);
        });
    }, [id]);

    if (food === null) return <h1>Loading...</h1>;

    function handleDeleteFood()
    {
        if (window.confirm(`Are you sure you want to delete this food (${food.name})?`))
        {
            const service = new FoodsService();

            service.deleteFood(food).then(() => {
                setToastMessage("Food was successfully deleted");
                setShowToast(true);
            });
        }
    }

    return <>
        <div className="card my-4">
            <div className="card-body">
                <div className="d-flex align-items-center justify-content-between">
                    <div className="d-flex align-items-center column-gap-3">
                        <h1 className="m-0">{food.name}</h1>

                        <Link role="button" title="Edit food" to={`/foods/${id}/edit`} className="btn btn-primary">Edit</Link>
                    </div>

                    <button type="button" title="Delete food" className="btn btn-danger" onClick={handleDeleteFood}>Delete</button>
                </div>

                <p className="mb-2">{food.description}</p>
                <p className="mb-2">Servings: {food.servings}</p>
                <div className="">
                    <EatFoodForm food={food} setFood={setFood} />
                </div>

                <div>
                    <h2>
                        Nutrition per serving:
                    </h2>
                    <table className="table">
                        <tbody>
                            <tr>
                                <th scope="row">Calories</th>
                                <td><span>{food.calories}</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Protein</th>
                                <td><span>{food.gramsProtein} g</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Saturated Fat</th>
                                <td><span>{food.gramsSaturatedFat} g</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Trans Fat</th>
                                <td><span>{food.gramsTransFat} g</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Cholesterol</th>
                                <td><span>{food.milligramsCholesterol} mg</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Sodium</th>
                                <td><span>{food.milligramsCholesterol} mg</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Total Carbs</th>
                                <td><span>{food.gramsTotalCarbs} g</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Dietary Fiber</th>
                                <td><span>{food.gramsDietaryFiber} g</span></td>
                            </tr>
                            <tr>
                                <th scope="row">Total Sugars</th>
                                <td><span>{food.gramsTotalSugars} g</span></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        

        <div>
            <Link to="/foods">Back to foods</Link>
        </div>
    </>
}
