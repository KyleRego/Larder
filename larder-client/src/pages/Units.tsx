import { Link } from "react-router-dom";
import UnitsTable from "../components/tables/UnitsTable";
import { useContext, useEffect, useState } from "react";
import { UnitsContext } from "../contexts/UnitsContext";
import { UnitDto } from "../types/UnitDto";
import { apiClient } from "../util/axios";
import { UnitSortOptions } from "../types/UnitSortOptions";
import SearchBox from "../components/SearchBox";
import ActionBar from "../ActionBar";

export default function Units() {
    const { units, setUnits } = useContext(UnitsContext);
    const [sortOrder, setSortOrder] = useState(UnitSortOptions.Name);
    const [searchParam, setSearchParam] = useState("");

    useEffect(() => {
        async function getUnits() {
            const response = await apiClient.get<UnitDto[]>("api/units", 
                { params: {sortOrder: sortOrder, search: searchParam} })

            setUnits(response.data);
        }

        getUnits();
    }, [sortOrder, searchParam])

    function handleSearchChange(e: React.ChangeEvent<HTMLInputElement>) {
        setSearchParam(e.currentTarget.value);
    }

    return (
        <div className="h-100 d-flex flex-column">
            <div className="container mt-2 d-flex align-items-end column-gap-5 flex-wrap row-gap-1 px-4 pt-2 pb-4">
                <h1>Units</h1>

                <SearchBox handleOnChange={handleSearchChange} /> 
            </div>

            <div className="container flex-grow-1">
                <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />
            </div>

            <ActionBar>
                <div className="d-flex justify-content-center">
                    <Link className="btn btn-secondary border-black" to={"/units/new"}>New unit</Link>
                </div>
            </ActionBar>
        
        </div>
    );
}
