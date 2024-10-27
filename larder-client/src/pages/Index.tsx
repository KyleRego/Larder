import { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthedContext } from "../contexts/AuthedContext";

export default function Index() {
    const { authed } = useContext(AuthedContext)

    function handleCreateDemo() {
        // TODO
    }

    return (
        <div className="text-center">
            <h1 className="my-4">Larder</h1>
            <p className="mb-4">
                Larder is an inventory taking app with a focus on foods, ingredients, cooking, and nutrition. It is free and open source: <a href="https://github.com/KyleRego/Larder">Larder source code</a>
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
        </div>
    );
}
