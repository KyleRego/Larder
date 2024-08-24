import ConsumeFoodForm from "./ConsumeFoodForm";

export default function FoodsCards({foods, setFoods})
{
    const cards = foods.map(food => {
            return <div className="mb-3 card shadow-sm" key={food.id} style={{maxWidth: "20rem"}}>
                        <div className="card-body">
                            <h5 className="card-title">{food.name}</h5>
                            <p className="mb-2">{food.description}</p>
                            <p className="mb-2">Servings: {food.servings}</p>
                            <div>
                                <ConsumeFoodForm food={food} foods={foods} setFoods={setFoods} />
                            </div>
                        </div>
                    </div>;
        }
    );

    return <>
        <div className="d-flex justify-content-start column-gap-3 align-items-center flex-wrap">
            {cards}
        </div>
    </>
}
