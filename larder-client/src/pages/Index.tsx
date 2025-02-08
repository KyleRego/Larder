import { useContext } from "react";
import { AuthedContext } from "../contexts/AuthedContext";
import { useApiRequest } from "../hooks/useApiRequest";
import { useNavigate } from "react-router";
import ActionBar from "../ActionBar";

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
        navigate("/items");
    }

    return (
        <div className="text-center fs-4 d-flex flex-column h-100">
            <div className="container flex-grow-1">
                <h1 className="my-4">Larder</h1>
                <p className="mb-4">
                    Larder is an inventory taking app with a focus on foods, ingredients, cooking, and nutrition. It is free and open source: <a href="https://github.com/KyleRego/Larder">Larder source code</a>
                </p>
                <p className="">
                    Larder is not ready to use yet! It is being developed - please check back later.
                </p>
            </div>

            <ActionBar>
                {authed === false &&
                    <button onClick={handleCreateDemo}
                        type="button" className="btn btn-outline-light">
                    Try it out!
                </button>
                }
            </ActionBar>
        </div>
    );
}
