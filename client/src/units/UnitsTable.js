import SortingTableHeader from "../components/OrderableTableHeader";

export default function UnitsTable({data, sortOrder, setSortOrder})
{
    const tableRows = data.map(unit => TableRow(unit));

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
            <th scope="row">{unit.name}</th>
            <td>{unit.type}</td>
        </tr>
    )
}