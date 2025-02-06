import { Dispatch, SetStateAction, useState } from "react";
import ItemsTable from "../components/tables/ItemsTable";
import { Link } from "react-router-dom";
import SearchBox from "../components/SearchBox";
import FoodsTable from "../components/tables/FoodsTable";
import ActionBar from "../ActionBar";

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
        <div className="h-100 d-flex flex-column">
            <div className="mt-2 container d-flex align-items-end column-gap-5 flex-wrap row-gap-1 px-4 pt-2 pb-4">
                <h1>Items</h1>

                <TableVersionDropdown currentVariant={currentTable} setCurrentTable={setCurrentTable} />

                <SearchBox handleOnChange={handleSearchChange} />
            </div>

            <div className="table-responsive flex-grow-1 container">
                {renderCurrentTable()}
            </div>

            <ActionBar>
                <div className="d-flex justify-content-center">
                    <Link className="btn btn-outline-light text-black border-black" to={"/items/new"}>
                        New item
                    </Link>
                </div>
            </ActionBar>
        </div>
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
            <button className="btn btn-light dropdown-toggle border-black"
                    type="button" aria-expanded="false"
                onClick={() => setExpanded(!expanded)}    >
                {currentVariant}
            </button>
            <ul className={`dropdown-menu ${expanded === true ? "d-block" : "d-none"}`}>
                {dropdownOptions}
            </ul>
        </div>;
}
