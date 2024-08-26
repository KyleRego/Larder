import ConsumeFoodForm from "./ConsumeFoodForm";

export default function FoodCard({food, setFood})
{
    return <div className="mb-3 card shadow-sm" key={food.id}>
                        <div className="card-body">
                            <h5 className="card-title">{food.name}</h5>
                            <p className="mb-2">{food.description}</p>
                            <p className="mb-2">Servings: {food.servings}</p>
                            <div>
                                <ConsumeFoodForm food={food} setFood={setFood} />
                            </div>
                        </div>
                    </div>;
}
