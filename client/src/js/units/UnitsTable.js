import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";

export default function UnitsTable({units, sortOrder, setSortOrder})
{
    const tableRows = units.map(unit => TableRow(unit));

    return (
        <table className="unitsTable">
            <caption>
                Units
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
                {unit.type}
            </td>
        </tr>
    )
}