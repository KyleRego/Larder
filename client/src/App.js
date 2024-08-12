import "./App.css";
import Nav from "./Nav";
import { Outlet } from "react-router-dom";
import AppToast from "./components/AppToast";

import { useState } from "react"

export default function App() {
    const [toastMessage, setToastMessage] = useState("");
    const [showToast, setShowToast] = useState(false);

    return (
        <div className="app">
            <Nav />
            <div className="container">
                <Outlet context={[setToastMessage, setShowToast]} />
                <AppToast message={toastMessage} show={showToast} setShow={setShowToast} />
            </div>
        </div>
    );
}
