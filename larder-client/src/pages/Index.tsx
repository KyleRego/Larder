import { useContext } from "react";
import { AuthedContext } from "../contexts/AuthedContext";
import { useApiRequest } from "../hooks/useApiRequest";
import { useNavigate } from "react-router";

export default function Index() {
    const { authed, setAuthed } = useContext(AuthedContext)
    const { handleRequest } = useApiRequest();
    const navigate  = useNavigate();

    async function handleCreateDemo() {
        await handleRequest({
            method: "post",
            url: "/api/Demos"
        });

        setAuthed(true);
        navigate("/items/new");
    }

    return (
        <div className="text-center fs-4">
            <h1 className="my-4">Larder</h1>
            <p className="mb-4">
                Larder is an inventory taking app with a focus on foods, ingredients, cooking, and nutrition. It is free and open source: <a href="https://github.com/KyleRego/Larder">Larder source code</a>
            </p>
            <p className="">
                Larder is not ready to use yet! It is being developed - please check back later.
            </p>

            {authed === false
            ?
            <div className="my-4 d-flex flex-wrap justify-content-center">
                <button onClick={handleCreateDemo} type="button" className="btn btn-lg btn-outline-primary">
                    Try it out!
                </button>
            </div>
            :
            ""
            }
        </div>
    );
}
