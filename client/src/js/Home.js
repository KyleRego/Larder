import { Link } from "react-router-dom"

export default function Home()
{
    return <>
        <h1 className="text-center">Larder</h1>
        <p>Larder is a free software application (<a href="https://github.com/KyleRego/Larder/blob/main/LICENSE.txt">GNU AGPLv3</a>)
        for tracking your inventory of foods and ingredients; as you cook and eat them, the app also helps track
        some useful personal nutritional statistics.</p>

        <div className="d-flex flex-wrap justify-content-center column-gap-3">
            <Link className="btn btn-outline-primary" to={"register"}>Register</Link>

            <Link className="btn btn-outline-primary" to={"login"}>Login</Link>
        </div>
    </>;
}
