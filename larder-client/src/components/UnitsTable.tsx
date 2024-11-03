import { useNavigate } from "react-router-dom";
import { UnitDto } from "../types/UnitDto";
import { Dispatch, SetStateAction } from "react";
import SortingTableHeader from "./SortingTableHeader";
import { UnitSortOptions } from "../types/UnitSortOptions";

export default function UnitsTable({units, sortOrder, setSortOrder}
        : { units: UnitDto[],
            sortOrder: UnitSortOptions,
            setSortOrder: Dispatch<SetStateAction<UnitSortOptions>>}) {

    const unitRows = units.map(u => <UnitRow key={u.id} unit={u} />);

    return (
        <>
            <table className="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <SortingTableHeader<UnitSortOptions> ascending={UnitSortOptions.Name}
                                            descending={UnitSortOptions.Name_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Name" />
                        <SortingTableHeader<UnitSortOptions> ascending={UnitSortOptions.Type}
                                            descending={UnitSortOptions.Type_Desc}
                                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                                            headerText="Type" />
                    </tr>
                </thead>
                <tbody>
                    {unitRows}
                </tbody>
            </table>
        </>
    );
}

function UnitRow({unit} : {unit: UnitDto}) {
    const navigate = useNavigate();

    function handleRowClick() {
        navigate(`/units/${unit.id}`)
    }

    return (
        <tr role="button" onClick={handleRowClick}>
            <th scope="row">{unit.name}</th>
            <td>{UnitDto.getType(unit)}</td>
        </tr>
    );
}
