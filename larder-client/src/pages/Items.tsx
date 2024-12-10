import { Dispatch, SetStateAction, useState } from "react";
import ItemsTable from "../components/tables/ItemsTable";
import { Link } from "react-router-dom";
import SearchBox from "../components/SearchBox";
import FoodsTable from "../components/tables/FoodsTable";

enum TableVersions {
    AllItems = "All Items",
    Foods = "Foods",
    Ingredients = "Ingredients"
}

export default function Items() {
    
    const [currentTable, setCurrentTable] = useState<TableVersions>(TableVersions.AllItems);
    const [searchParam, setSearchParam] = useState("");

    function renderCurrentTable() {
        switch (currentTable) {
            case TableVersions.AllItems:
                return <ItemsTable searchParam={searchParam} />
            case TableVersions.Foods:
                return <FoodsTable searchParam={searchParam} />
            case TableVersions.Ingredients:
                return <div>Ingredients table placeholder</div>
        }
    }

    function handleSearchChange(e: React.ChangeEvent<HTMLInputElement>) {
        setSearchParam(e.currentTarget.value);
    }

    return (
        <>
            <div className="page-flex-header align-items-end">
                <h1>Items</h1>

                <SearchBox handleOnChange={handleSearchChange} />

                <TableVersionDropdown currentVariant={currentTable} setCurrentTable={setCurrentTable} />

                <Link className="btn btn-secondary border-black" to={"/items/new"}>New item</Link>
            </div>

            <div className="table-responsive">
                {renderCurrentTable()}
            </div>
        </>
    );
}

function TableVersionDropdown({currentVariant, setCurrentTable}
                        : { currentVariant: TableVersions,
                            setCurrentTable: Dispatch<SetStateAction<TableVersions>> }) {
    const [expanded, setExpanded] = useState(false);

    const dropdownOptions = Object.values(TableVersions).filter((mem) => mem != currentVariant).map((mem, indx) => {
        return <li key={indx} onClick={() => {
            setCurrentTable(mem);
            setExpanded(false);
        }}>
            <a className="dropdown-item" href="#">{mem}</a>
        </li>;
    });

    return <div className="dropdown">
            <button className="btn btn-secondary dropdown-toggle border-black" type="button" aria-expanded="false"
                onClick={() => setExpanded(!expanded)}    >
                {currentVariant}
            </button>
            <ul className={`dropdown-menu ${expanded === true ? "d-block" : "d-none"}`}>
                {dropdownOptions}
            </ul>
        </div>;
}
