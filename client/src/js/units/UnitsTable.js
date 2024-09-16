import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";
import UnitHelpers from "./UnitHelpers";

export default function UnitsTable({units, sortOrder, setSortOrder})
{
    const tableRows = units.map(unit => TableRow(unit));

    return (
        <table className="unitsTable">
            <caption>
                Compatible units must be the same type for successful conversions.
            </caption>

            <thead>
                <tr>
                    <SortingTableHeader columnName="Name" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <SortingTableHeader columnName="Type" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                </tr>
            </thead>

            <tbody>
                {tableRows}
            </tbody>
            
        </table>
    );
}

function TableRow(unit)
{
    return (
        <tr key={unit.id}>
            <th scope="row">
                <Link to={`/units/${unit.id}`}>{unit.name}</Link>
            </th>
            <td>
                {UnitHelpers.UnitTypeEnumValueToText(unit.type)}
            </td>
        </tr>
    )
}