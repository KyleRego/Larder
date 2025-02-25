import { useEffect, useState } from "react";
import BreadCrumbs from "../Breadcrumbs";
import { ItemDto } from "../types/ItemDto";
import { apiClient } from "../util/axios";
import Loading from "../components/Loading";
import QuantitySpan from "../components/QuantitySpan";
import NutritionTable from "../components/tables/NutritionTable";

export default function FoodLog() {
    const [consumedFoods, setConsumedFoods] = useState<ItemDto[] | null>(null);
    const [selectedDate, setSelectedDate] = useState<Date>(new Date());

    useEffect(() => {
        async function getConsumedFoods() {
            const dateAsString: string = selectedDate.toISOString().split("T")[0];
            const response = await apiClient.get<ItemDto[]>("api/Foods/ConsumedFoods", 
                { params: {day: dateAsString} })

            setConsumedFoods(response.data);
        }

        getConsumedFoods();
    }, [selectedDate])

    function changeDate(days: number) {
        setSelectedDate(prevDate => {
            const newDate = new Date(prevDate);
            newDate.setDate(prevDate.getDate() + days);
            return newDate;
        });
    }

    return <div>
        <BreadCrumbs>
            <li className="breadcrumb-item active">
                <h1 className="d-inline fs-6">
                    Food log
                </h1>
            </li>
        </BreadCrumbs>

        <div className="my-4 container">
            <div className="d-flex justify-content-between column-gap-3 align-items-center">
                <button onClick={() => changeDate(-1)}
                        className="btn btn-outline-light"
                        type="button">
                    ⬅ Yesterday
                </button>
                <h3 className="d-inline m-0">
                    {selectedDate.toDateString()}
                </h3>
                <button onClick={() => changeDate(1)}
                        className="btn btn-outline-light"
                        type="button">
                    ➡ Tomorrow
                </button>
            </div>

            <div className="my-4">
                {consumedFoods === null ?
                <Loading />
                :
                    <ol className="list-group">
                        {consumedFoods.map(f => <ListItem key={f.id} f={f} />)}
                    </ol>
                }
            </div>
        </div>
    </div>
}

function ListItem({f} : {f: ItemDto}) {
    const [expanded, setExpanded] = useState<boolean>(false);

    return <li className="list-group-item">
            <div className="d-flex justify-content-between align-items-center"
                role="button" onClick={() => setExpanded(!expanded)} >
                <div className="fs-5 d-flex column-gap-1">
                    <QuantitySpan quantity={f.quantity!} />
                    {f.name}
                </div>
                <div>
                    {expanded ? "➖" : "➕"}
                </div>
            </div>
            { expanded === true &&
            <div>
                <NutritionTable nutrition={f.nutrition!} />
            </div>
            }
        </li>;
}