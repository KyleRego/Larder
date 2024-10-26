import { useContext, useState } from "react";
import { Link } from "react-router-dom";
import { AuthedContext } from "./contexts/AuthedContext";
import './NavBar.css';
import { apiClient } from "./util/axios";

export function NavBar() {
    const [collapsed, setCollapsed] = useState(false);
    const { authed, setAuthed } = useContext(AuthedContext);

    function handleLogout() {
        apiClient.post("/logout")
                .then(() => setAuthed(false));
    }

    return (
        <nav className="navbar navbar-expand-lg sticky-top p-3 mb-4">
            <div className="container">
                <Link to="/" className="navbar-brand">Larder</Link>
                {authed === true ?
                <>
                <div className="d-flex column-gap-3">
                    <button onClick={() => setCollapsed(!collapsed)} className="navbar-toggler" type="button" title="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="d-lg-none">
                        <button onClick={handleLogout} type="button" className="btn btn-outline-light text-black">Log out</button>
                    </div>
                </div>
                <div className={`collapse navbar-collapse ${collapsed === false && "show"}`} id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <Link to="/items" className="nav-link">Items</Link>
                        </li> 
                    </ul>
                    <div className="d-none d-lg-block">
                        <button onClick={handleLogout} type="button" className="btn btn-outline-light text-black">Log out</button>
                    </div>
                </div>
                </> :
                <>
                <div className="d-flex column-gap-3">
                    <Link to="/login" className="btn btn-outline-light text-black">Login</Link>
              
                    <Link to="/register" className="btn btn-outline-light text-black">Register</Link>  
                </div> 
                </>
                }          
            </div>
        </nav>
    );
}
