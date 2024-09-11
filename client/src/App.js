import { Outlet } from "react-router-dom";

import "./App.css";
import Nav from "./Nav";
import AppToast from "./js/components/AppToast";

import { useState, useEffect, createContext } from "react"
import UnitsService from "./js/services/UnitsService";

export const UnitsContext = createContext([]);
export const AuthedContext = createContext(false);

export default function App() {
    const [authed, setAuthed] = useState(false);
    const [units, setUnits] = useState([]);

    const [toastMessage, setToastMessage] = useState("");
    const [showToast, setShowToast] = useState(false);

    useEffect(() => {
        const unitsService = new UnitsService();

        unitsService.getUnits().then((res) => {
            setUnits(res);
            setAuthed(true);
            console.log("authed set to true");
        }).catch((error) => {
            setUnits([]);
            setAuthed(false);
        })
    }, []);

    return (
        <AuthedContext.Provider value={authed}>
            <UnitsContext.Provider value={units}>
                <div className="app">
                    <Nav setAuthed={setAuthed} />
                    <div className="container">
                        <Outlet context={[setToastMessage, setShowToast, setAuthed]} />

                        <AppToast message={toastMessage} show={showToast} setShow={setShowToast} />
                    </div>
                </div>
            </UnitsContext.Provider>
        </AuthedContext.Provider> 
    );
}
