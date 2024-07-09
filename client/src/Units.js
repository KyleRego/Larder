import { useState, useEffect } from "react";
import UnitsService from "./services/UnitsService"
import "./Units.css"

export default function Units()
{
    const [units, setUnits] = useState([]);

    function updateUnitsData(sortOrder)
    {
        console.log("hello world");
        const service = new UnitsService();

        service.getUnits(sortOrder).then(result => {
            setUnits(result);
        })
    }

    useEffect(() =>
    {
        const service = new UnitsService();

        service.getUnits().then(result => {
            setUnits(result);
        })
    }, []);

    return (
        <>
            <h1 className="tabHeading">
                Units
            </h1>

            <UnitsTable data={units} updateUnitsData={updateUnitsData} />
        </>
    )
}

function UnitsTable({data, updateUnitsData})
{
    const tableRows = data.map(unit => TableRow(unit));

    return (
        <table className="unitsTable">
            <caption>
                Units
            </caption>
            <thead>
                <tr>
                    <th>Id</th>
                    <th onClick={() => updateUnitsData("Name_Desc")}>Name</th>
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
            <td>{unit.id}</td>
            <td>{unit.name}</td>
        </tr>
    )
}
