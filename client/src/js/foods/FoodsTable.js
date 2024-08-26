import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";

import FoodsService from "../services/FoodsService";
import { useState } from "react";

import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";

// setFoods is passed in so that when the food amount is edited in cell,
// the entire foods state can be updated, to change the amount of that one food
export default function FoodsTable({foods, setFoods, sortOrder, setSortOrder})
{
    let rows = foods.map(food => FoodRow(food, foods, setFoods));

    return <>
        <table className="foodsTable">
            <caption>Servings of ready to eat food.</caption>
            <thead>
                <tr>
                    <SortingTableHeader columnName="Name" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <SortingTableHeader columnName="Servings" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <SortingTableHeader columnName="Calories" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                </tr>
            </thead>

            <tbody>
                {rows}
            </tbody>
        </table>
    </>
}

function FoodRow(food, foods, setFoods)
{
    return (
        <tr key={food.id}>
            <th scope="row">
                <Link to={`/foods/${food.id}`}>
                    {food.name}
                </Link>
            </th>

            <FoodAmountTableCell food={food} foods={foods} setFoods={setFoods} />

            <td>{food.calories}</td>
        </tr>
    );
}

function FoodAmountTableCell({food, foods, setFoods})
{
    const [editing, setEditing] = useState(false);

    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);
        const newServings = formData.get("servings");

        const foodServingsDto = {
            foodId: food.id,
            servings: newServings
        };

        const service = new FoodsService();
        await service.patchFood(foodServingsDto).then(() => {
            const newFoods = structuredClone(foods);
            
            for (let i = 0; i < newFoods.length; i += 1)
            {
                if (newFoods[i].id === food.id)
                {
                    newFoods[i].servings = newServings
                }
            }

            setFoods(newFoods);
            setEditing(false);
        });
    }

    if (editing === true)
    {
        return <td className="py-0">
            <form onSubmit={handleSubmit}>
                <div className="m-0 d-flex flex-wrap column-gap-1 row-gap-1 align-items-center">
                    <input name="servings" type="number" step="any" defaultValue={food.servings}></input>
                    <label hidden htmlFor="servings"></label>

                    <button className="btn btn-primary btn-sm" type="submit" title="Done">
                        <MdDone />
                    </button>

                    <button onClick={() => setEditing(false)} className="btn btn-secondary btn-sm" type="button" title="Cancel">
                        Cancel
                    </button>
                </div>
            </form>
        </td>
    }
    else
    {
        return <td className="py-0">
            <div className="m-0 d-flex flex-wrap column-gap-1 row-gap-1 align-items-center">
                <span>{food.servings}</span>
                <CiEdit className="w-5 h-5" role="button" onClick={() => setEditing(true)} title="Edit" />
            </div>
        </td>
    }
}
