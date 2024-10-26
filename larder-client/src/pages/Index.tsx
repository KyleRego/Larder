import { Link } from "react-router-dom";

export default function Index() {
    return <>
        <div>
            hello world
        </div>
        <div>
            <Link to={"/items"}>Items</Link>
        </div>
        <div>
            <Link to={"/items/new"}>New item</Link>
        </div>
        <div>
            <Link to={"/login"}>Login</Link>
        </div>
        <div>
            <Link to={"/register"}>Register</Link>
        </div>
        <div>
            <Link to={"/logout"}>Logout</Link>
        </div>
    </>;
}
