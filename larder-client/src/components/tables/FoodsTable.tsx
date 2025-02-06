import { ReactNode, useEffect, useState } from "react";
import { ItemDto } from "../../types/ItemDto";
import { FoodSortOptions } from "../../types/FoodSortOptions";
import SortingTableHeader from "../SortingTableHeader";
import { useNavigate } from "react-router";
import { apiClient } from "../../util/axios";
import { NutritionDto } from "../../types/NutritionDto";
import Loading from "../Loading";

export default function FoodsTable({searchParam} : {searchParam: string }) {
    const [items, setItems] = useState<ItemDto[] | null>(null);         
    const [sortOrder, setSortOrder] = useState(FoodSortOptions.Name);

    useEffect(() => {
        apiClient.get<ItemDto[]>("/api/foods",
            { params: {search: searchParam, sortOrder: sortOrder}})
            .then(res => setItems(res.data))
            .catch(error => console.log(error));
    }, [searchParam, sortOrder])

    if (items === null) return <Loading />

    const foodRows = items.map(item => {
        return <NutritionRow key={item.id} nutrition={item.nutrition!} item={item} />
    });

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

function NutritionRow({item, nutrition}
        : {item: ItemDto, nutrition: NutritionDto}) : ReactNode {
    const navigate = useNavigate();
    const itemId: string = item.id!

    function handleRowClick() {
        navigate(`/items/${itemId}`);
    }

    return (
        <tr role="button" onClick={handleRowClick} id={itemId} >
            <th scope="row" style={{maxWidth: "10rem", overflowX: "hidden"}}>
                {item.name}
            </th>
            <td>{String(nutrition.calories)}</td>
            <td>{String(nutrition.gramsProtein)}</td>
            <td>{String(nutrition.gramsTotalCarbs)}</td>
            <td>{String(nutrition.gramsTotalFat)}</td>
        </tr>
    );
}
