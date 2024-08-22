import { NavLink } from "react-router-dom";
import { MdOutlineFoodBank } from "react-icons/md";
import "./Nav.css";

import { useState, useEffect } from "react";

export default function Nav()
{
    const [showCollapsibleNavbar, setShowCollapsibleNavbar] = useState(false);
    const toggleCollapsibleNavbar = () => setShowCollapsibleNavbar(!showCollapsibleNavbar);

    // const navData = [
    //     ["Foods", "foods"],
    //     ["Units", "units"],
    //     ["Conversions", "conversions"],
    //     ["Recipes", "recipes"],
    //     ["Ingredients", "ingredients"],
    //     ["Notes", "notes"]
    // ]

    // const navListItems = navData.map(d => {
    //     return <li key={d[0]} className="navOption">
    //         <NavLink to={d[1]} className={({ isActive, isPending}) => {
    //             return isPending ? "navLink" : isActive ? "activeNavLink" : "navLink"
    //         }}  >
    //             {d[0]}
    //         </NavLink>
    //     </li>
    // });

    return (
        // <nav className="appNav">
        //     <h2 className="navHeading">
        //         <span className="flex justify-content-center align-items-center">
        //             <MdOutlineFoodBank />
        //             <span className="pl-2">
        //                 Larder
        //             </span>
        //         </span>
        //     </h2>
        //     <ol className="navList">
        //         {navListItems}
        //     </ol>
        // </nav>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <a className="navbar-brand ms-4" href="/">Larder</a>
            <button className="navbar-toggler me-4" onClick={toggleCollapsibleNavbar} type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
            </button>

            <div className={`collapse navbar-collapse ${showCollapsibleNavbar && "show"}`} id="navbarSupportedContent">
                <ul className="ms-4 navbar-nav mr-auto">
                <li className="nav-item active">
                    <a className="nav-link" href="/foods">Foods <span className="sr-only"></span></a>
                </li>
                <li className="nav-item">
                    <a className="nav-link" href="/ingredients">Ingredients</a>
                </li>
                {/* <li className="nav-item dropdown">
                    <a className="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Dropdown
                    </a>
                    <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                    <a className="dropdown-item" href="#">Action</a>
                    <a className="dropdown-item" href="#">Another action</a>
                    <div className="dropdown-divider"></div>
                    <a className="dropdown-item" href="#">Something else here</a>
                    </div>
                </li>
                <li className="nav-item">
                    <a className="nav-link disabled">Disabled</a>
                </li> */}
                </ul>
                {/* <form className="form-inline my-2 my-lg-0">
                <input className="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" />
                <button className="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </form> */}
            </div>
            </nav>
    )
}