import { Dispatch, ReactNode, SetStateAction } from "react";
import { ItemDto } from "../../types/Item";
import { FoodSortOptions } from "../../types/FoodSortOptions";
import SortingTableHeader from "../SortingTableHeader";

export default function FoodsTable({items, sortOrder, setSortOrder}
            : {items: ItemDto[],
                sortOrder: FoodSortOptions,
                setSortOrder: Dispatch<SetStateAction<FoodSortOptions>>
            }) {

    const foodRows = items.map(item => {
        return <FoodRow key={item.id} item={item} />
    });

    return (
        <table className="table table-striped text-break">
            <caption>
                Your foods
            </caption>
            <thead>
                <tr>
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.Name}
                                            descending={FoodSortOptions.Name_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Name" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.Servings}
                                            descending={FoodSortOptions.Servings_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Servings" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.Calories}
                                            descending={FoodSortOptions.Calories_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Calories" />
                    <SortingTableHeader<FoodSortOptions> ascending={FoodSortOptions.TotalCalories}
                                            descending={FoodSortOptions.TotalCalories_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Total Calories" />
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
    return (
        <tr>
            <th scope="row">{item.name}</th>
            <td>{String(item.food!.servings)}</td>
            <td>{String(item.food!.calories)}</td>
            <td>{String(item.food!.totalCalories)}</td>
            <td>{String(item.food!.gramsProtein)}</td>
            <td>{String(item.food!.gramsTotalCarbs)}</td>
            <td>{String(item.food!.gramsTotalFat)}</td>
        </tr>
    );
}
