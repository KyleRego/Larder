import FoodEater from "./FoodEater";

export default function FoodsCards({foods})
{
    const cards = foods.map(food => {
            return <div className="mb-3 card shadow-sm" key={food.id} style={{maxWidth: "20rem"}}>
                        <div className="card-body">
                            <h5 className="card-title">{food.name}</h5>
                            <p className="mb-2">{food.description}</p>
                            <p className="mb-2">Amount: {food.amount}</p>
                            <div>
                                <FoodEater food={food} />
                            </div>   
                        </div>
                    </div>;
        }
    );

    return <>
        <div className="d-flex justify-content-around align-items-center flex-wrap">
            
                {cards}
            
        </div>
    </>
}
