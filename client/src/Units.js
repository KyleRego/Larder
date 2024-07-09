import { useState, useEffect } from "react";
import { TiArrowUnsorted } from "react-icons/ti";
import { TiArrowSortedUp } from "react-icons/ti";
import { TiArrowSortedDown } from "react-icons/ti";
import UnitsService from "./services/UnitsService"
import "./Units.css"
import "./Utility.css"

export default function Units()
{
    const [units, setUnits] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const service = new UnitsService();

        service.getUnits(sortOrder).then(result => {
            setUnits(result);
        })
    }, [sortOrder]);

    return (
        <>
            <h1 className="tabHeading">
                Units
            </h1>

            <UnitsTable data={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />
        </>
    )
}

function UnitsTable({data, sortOrder, setSortOrder})
{
    const tableRows = data.map(unit => TableRow(unit));

    return (
        <table className="unitsTable">
            <caption>
                Units
            </caption>

            <thead>
                <tr>
                    <NameHeaderCell sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <TypeHeaderCell sortOrder={sortOrder} setSortOrder={setSortOrder} />
                </tr>
            </thead>

            <tbody>
                {tableRows}
            </tbody>
            
        </table>
    );
}

function NameHeaderCell({sortOrder, setSortOrder})
{
    function handleClick()
    {
        if (sortOrder === "Name")
        {
            setSortOrder("Name_Desc")
        }
        else
        {
            setSortOrder("Name");
        }
    }

    let icon;

    if (sortOrder === "Name")
    {
        icon = <TiArrowSortedUp />
    }
    else if (sortOrder === "Name_Desc")
    {
        icon = <TiArrowSortedDown />
    }
    else
    {
        icon = <TiArrowUnsorted />
    }

    return (
        <th scope="col" onClick={handleClick}>
            <div class="flex justify-around cursor-pointer">
                Name
                {icon}
            </div>
        </th>
    )
}

function TypeHeaderCell({sortOrder, setSortOrder})
{
    function handleClick()
    {
        if (sortOrder === "Type")
        {
            setSortOrder("Type_Desc")
        }
        else
        {
            setSortOrder("Type");
        }
    }

    let icon;

    if (sortOrder === "Type")
    {
        icon = <TiArrowSortedUp />
    }
    else if (sortOrder === "Type_Desc")
    {
        icon = <TiArrowSortedDown />
    }
    else
    {
        icon = <TiArrowUnsorted />
    }

    return (
        <th scope="col" onClick={handleClick}>
            <div class="flex justify-around cursor-pointer">
                Type
                {icon}
            </div>
        </th>
    )
}

function TableRow(unit)
{
    return (
        <tr key={unit.id}>
            <td scope="row">{unit.name}</td>
            <td>{unit.type}</td>
        </tr>
    )
}
