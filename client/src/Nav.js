import "./Nav.css";

import { useState, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";

import { AuthedContext } from "./AuthedContext";
import IdentityService from "./js/identity/IdentityService";

export default function Nav()
{
    const navigate = useNavigate();
    const { authed, setAuthed } = useContext(AuthedContext);
    const [showCollapsibleNavbar, setShowCollapsibleNavbar] = useState(false);
    const toggleCollapsibleNavbar = () => setShowCollapsibleNavbar(!showCollapsibleNavbar);

    function handleLogout() {
        const identityService = new IdentityService();

        identityService.postLogout().then(() => {
            setAuthed(false);
            navigate("/")
        }).catch((error) => {
            console.log(error);
        });
    }

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                <Link className="navbar-brand py-3 ms-4" to={"/"}>Larder</Link>
                {authed === true &&
                <>
                <button className="navbar-toggler" onClick={toggleCollapsibleNavbar} type="button"
                                data-toggle="collapse" data-target="#navbarSupportedContent"
                                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div className={`collapse navbar-collapse ${showCollapsibleNavbar && "show"}`} id="navbarSupportedContent">
                    <div className="w-100 d-flex justify-content-between align-items-center">
                        <ul className="ms-4 navbar-nav mr-auto">
                            <li className="nav-item active">
                                <Link className="nav-link" to={"/foods"}>Foods<span className="sr-only"></span></Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to={"/timeline"}>Nutrition timeline</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to={"/ingredients"}>Ingredients</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to={"/recipes"}>Recipes</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to={"/units"}>Units</Link>
                            </li>
                        </ul>

                        <div>
                            <button onClick={handleLogout} className="btn btn-outline-primary" type="button">
                                Logout
                            </button>
                        </div>
                    </div>
                </div>
                </>
                }
            </div>
        </nav>
    )
}
