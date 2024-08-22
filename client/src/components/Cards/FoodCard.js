import "./Cards.css";

export default function FoodCard({food})
{
    return <div className="card shadow-sm">
        <div>
            {food.name}
        </div>
    </div>
}
