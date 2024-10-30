import { RiDeleteBinLine } from "react-icons/ri";

export default function DeleteBtn({handleClick}: {handleClick: () => void}) {
    return (
        <button onClick={handleClick} type="button" className="btn btn-sm btn-outline-danger">
            <RiDeleteBinLine className="icon" />
        </button>
    );
}