import { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthedContext } from "../AuthedContext";
import DemoService from "./services/DemoService";
import { AlertContext } from "../AlertContext";
import UnitsService from "./services/UnitsService";
import { UnitsContext } from "../UnitsContext";

export default function Home() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { authed, setAuthed } = useContext(AuthedContext);
    const { setUnits } = useContext(UnitsContext);

    function handleCreateDemo() {
        const service = new DemoService();

        service.postCreateDemo().then(res => {
            setAlertMessage("Success! You are signed in as a demo user to try out Larder 🤍");
            const unitsService = new UnitsService();

            unitsService.getUnits().then((result) => {
                setUnits(result);
                setAuthed(true);
                navigate("/foods")
            })
        }).catch(error => {
            setAlertMessage("Something went wrong setting up a demo");
        });
    }

    return <>
        <h1 className="text-center my-4">Larder</h1>
        <p className="mb-4">
            Larder is a free software application (see the <a href="https://github.com/KyleRego/Larder">source code</a> and <a href="https://github.com/KyleRego/Larder/blob/main/LICENSE.txt">license</a>)
            for tracking your personal inventories of foods and ingredients; as you cook and eat them, Larder will track some nutritional statistics for you.
        </p>
        <p>
            Larder is not ready to use yet! It is being developed - please check back later.
        </p>

        {authed === false
        ?
        <div className="my-4 d-flex flex-wrap justify-content-center column-gap-3 row-gap-3">
            <Link className="btn btn-outline-primary" to={"register"}>Register</Link>

            <Link className="btn btn-outline-primary" to={"login"}>Login</Link>

            <button onClick={handleCreateDemo} type="button" className="btn btn-outline-primary">
                Try it out!
            </button>
        </div>
        :
        ""
        }
    </>;
}
