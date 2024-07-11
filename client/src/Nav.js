import { NavLink } from "react-router-dom";
import { MdOutlineFoodBank } from "react-icons/md";
import "./Nav.css";

export default function Nav()
{
    const navData = [
        ["Units", "units"],
        ["Conversions", "conversions"],
        ["Recipes", "recipes"],
        ["Ingredients", ""],
        ["Notes", "notes"]
    ]

    const navListItems = navData.map(d => {
        return <li key={d[0]} className="navOption">
            <NavLink to={d[1]} className={({ isActive, isPending}) => {
                return isPending ? "navLink" : isActive ? "activeNavLink" : "navLink"
            }}  >
                {d[0]}
            </NavLink>
        </li>
    });

    return (
        <nav className="appNav">
            <h2 className="navHeading">
                <span className="flex justify-content-center align-items-center">
                    <MdOutlineFoodBank />
                    <span className="pl-2">
                        Larder
                    </span>
                </span>
            </h2>
            <ol className="navList">
                {navListItems}
            </ol>
        </nav>
    )
}