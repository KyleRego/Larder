import { ReactNode, useEffect, useState } from "react";
import { ItemDto } from "../../types/ItemDto";
import { FoodSortOptions } from "../../types/FoodSortOptions";
import SortingTableHeader from "../SortingTableHeader";
import { useNavigate } from "react-router";
import { apiClient } from "../../util/axios";

export default function FoodsTable({searchParam} : {searchParam: string }) {

    const [items, setItems] = useState<ItemDto[]>([]);         
    const [sortOrder, setSortOrder] = useState(FoodSortOptions.Name);

    const foodRows = items.map(item => {
        return <FoodRow key={item.id} item={item} />
    });

    useEffect(() => {
        apiClient.get<ItemDto[]>("/api/foods", { params: {search: searchParam, sortBy: sortOrder}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortOrder])

    return (
        <table className="table table-striped table-hover">
            <caption>
                Your foods
            </caption>
            <thead>
                <tr>
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.Name}
                                            descending={FoodSortOptions.Name_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Name" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.Calories}
                                            descending={FoodSortOptions.Calories_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Calories" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.GramsProtein}
                                            descending={FoodSortOptions.GramsProtein_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Protein (g)" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.GramsTotalCarbs}
                                            descending={FoodSortOptions.GramsTotalCarbs_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Total carbs (g)" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.GramsTotalFat}
                                            descending={FoodSortOptions.GramsTotalCarbs_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Total fat (g)" />
                </tr>
            </thead>
            <tbody>
                {foodRows}
            </tbody>
        </table>
    );
}

function FoodRow({item} : {item: ItemDto}) : ReactNode {
    const navigate = useNavigate();

    function handleRowClick() {
        navigate(`/items/${item.id}`);
    }

    return (
        <tr role="button" onClick={handleRowClick} id={item.id!}>
            <th scope="row">{item.name}</th>
            <td>{String(item.nutrition!.calories)}</td>
            <td>{String(item.nutrition!.gramsProtein)}</td>
            <td>{String(item.nutrition!.gramsTotalCarbs)}</td>
            <td>{String(item.nutrition!.gramsTotalFat)}</td>
        </tr>
    );
}
