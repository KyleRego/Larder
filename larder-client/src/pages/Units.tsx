import { Link } from "react-router-dom";
import UnitsTable from "../components/UnitsTable";
import { useContext, useEffect, useState } from "react";
import { UnitsContext } from "../contexts/UnitsContext";
import { UnitDto } from "../types/UnitDto";
import { apiClient } from "../util/axios";
import { UnitSortOptions } from "../types/UnitSortOptions";
import SearchBox from "../components/SearchBox";

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
        <>
            <div className="page-flex-header">
                <h1>Units</h1>

                <SearchBox handleOnChange={handleSearchChange} />

                <Link className="btn btn-primary" to={"/units/new"}>New unit</Link>
            </div>

            <div className="mt-4">
                <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />
            </div>
        
        </>
    );
}
