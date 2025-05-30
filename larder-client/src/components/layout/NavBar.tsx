import { useContext, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthedContext } from "../../contexts/AuthedContext";
import { apiClient } from "../../util/axios";
import { MessageContext } from "../../contexts/MessageContext";
import { ApiResponseType } from "../../types/ApiResponse";

export function NavBar() {
    const [collapsed, setCollapsed] = useState(true);
    const { authed, setAuthed } = useContext(AuthedContext);
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

    function handleLogout() {
        // TODO: Use handleApiRequest here
        apiClient.post("/logout").then(() => {
            setAuthed(false);
            setCollapsed(true);
            setMessage({text: "You are now logged out.", type: ApiResponseType.Success});
            navigate("/");
        }).catch(error => console.error(error));
    }

    const logoutBtn =   <button id="logout-btn" onClick={handleLogout} type="button"
                                className="btn btn-outline-light">
                            Log out
                        </button>;

    return (
        <nav className="navbar navbar-expand-lg sticky-top p-3 bg-primary">
            <div className="container">
                <Link to="/" className="navbar-brand">Larder</Link>
                {authed === true ?
                <>
                <div className="d-flex column-gap-3 align-items-center">
                    <div className="d-lg-none">
                        <NewSomethingDropdown />
                    </div>
                    <button onClick={() => setCollapsed(!collapsed)} className="navbar-toggler" type="button" title="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="d-lg-none">
                        {logoutBtn}
                    </div>
                </div>
                <div className={`collapse navbar-collapse ${collapsed === false && "show"}`} id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <Link className="nav-link" to="/items-grid">Items</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/items">Tables</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/units">Units</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/food-log">Food log</Link>
                        </li>
                    </ul>
                    
                    <div className="d-none d-lg-block">
                        <div className="d-flex align-items-center column-gap-5">
                            <NewSomethingDropdown />
                            {logoutBtn}
                        </div>
                    </div>
                </div>
                </> :
                <>
                <div className="d-flex column-gap-3">
                    <Link id="login-btn" to="/login" className="btn btn-outline-light">Login</Link>
              
                    <Link id="register-btn" to="/register" className="btn btn-outline-light">Register</Link>  
                </div> 
                </>
                }          
            </div>
        </nav>
    );
}

function NewSomethingDropdown() {
    const [expanded, setExpanded] = useState(false);

    const newSomethings: string[][] = [
        ["item", "items/new"],
        ["unit", "units/new"]
    ];
    
    const dropdownOptions = newSomethings.map((data, index) => {
        return <Link className="dropdown-item" key={index} to={data[1]} onClick={() => setExpanded(!expanded)}>New {data[0]}</Link>;
    });
    
    return <div className="nav-item dropdown" id="new-something-dropdown">
            <a className="nav-link dropdown-toggle" title="New dropdown"
                href="#"
                role="button"
                        
                        onClick={() => setExpanded(!expanded)}>
                ➕
            </a>
            <div className={`dropdown-menu ${expanded === true ? "d-block" : "d-none"}`}
                style={{maxWidth: "12rem"}}   >
                {dropdownOptions}
            </div>
        </div>;
}