import { useContext, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthedContext } from "./contexts/AuthedContext";
import { apiClient } from "./util/axios";
import { MessageContext } from "./contexts/MessageContext";
import { ApiResponseType } from "./types/ApiResponse";

export function NavBar() {
    const [collapsed, setCollapsed] = useState(false);
    const { authed, setAuthed } = useContext(AuthedContext);
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

    function handleLogout() {
        apiClient.post("/logout").then(() => {
            setAuthed(false);
            setCollapsed(true);
            setMessage({text: "You are now logged out.", type: ApiResponseType.Success});
            navigate("/");
        }).catch(error => console.error(error));
    }

    const logoutBtn =   <button id="logout-btn" onClick={handleLogout} type="button"
                                className="btn btn-outline-light text-black border-black">
                            Log out
                        </button>;

    return (
        <nav className="navbar navbar-expand-lg sticky-top p-3 bg-primary">
            <div className="container">
                <Link to="/" className="navbar-brand">Larder</Link>
                {authed === true ?
                <>
                <div className="d-flex column-gap-3">
                    <button onClick={() => setCollapsed(!collapsed)} className="navbar-toggler" type="button" title="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="d-lg-none">
                        {logoutBtn}
                    </div>
                </div>
                <div className={`collapse navbar-collapse ${collapsed === false && "show"}`} id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item"><Link className="nav-link" to="/items">Items</Link></li>
                        <li className="nav-item"><Link className="nav-link" to="/units">Units</Link></li> 
                    </ul>
                    <div className="d-none d-lg-block">
                        {logoutBtn}
                    </div>
                </div>
                </> :
                <>
                <div className="d-flex column-gap-3">
                    <Link id="login-btn" to="/login" className="btn btn-outline-light text-black border-black">Login</Link>
              
                    <Link id="register-btn" to="/register" className="btn btn-outline-light text-black border-black">Register</Link>  
                </div> 
                </>
                }          
            </div>
        </nav>
    );
}
