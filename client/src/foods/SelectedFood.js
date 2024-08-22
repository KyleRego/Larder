import FoodEater from "./FoodEater"

export default function SelectedFood({food})
{
    return <div className="card shadow-lg">
            Selected food: {food.name}
            <FoodEater food={food} />
        </div>;
}