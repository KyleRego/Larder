import { useState, useEffect } from "react";
import UnitsService from "./services/UnitsService"
import "./Units.css"

function TableRow(unit)
{
    return (
        <tr>
            {unit}
        </tr>
    )
}

function Table({data: data})
{
    if (data.length !== undefined)
    {
        const tableRows = data.map(unit => TableRow(unit));

        return (
            <table className="unitsTable">
                <caption>
                    Units
                </caption>
                <thead>
                    <tr>
                        <th>Unit</th>
                        <th>Type</th>
                    </tr>
                    
                </thead>
                <tbody>
                    {tableRows}
                </tbody>
                
            </table>
        );
    }
}

export default function Units()
{
    const [units, setUnits] = useState([]);
    const service = new UnitsService();

    useEffect(() =>
    {
        service.getUnits().then(result => {
            setUnits(result);
        })
    }, []);

    return (
        (
            <>
                <h1 className="tabHeading">
                    Units
                </h1>

                <Table data={units} />
            </>
        )
    )
}
