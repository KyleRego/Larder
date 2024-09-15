import { useState, useEffect } from "react"
import { Outlet } from "react-router-dom";
import { AuthedContext } from "./AuthedContext";
import { AlertContext } from "./AlertContext";
import { UnitsContext } from "./UnitsContext";
import "./App.css";
import Nav from "./Nav";
import AppToast from "./js/components/AppToast";
import Alert from "./Alert";
import UnitsService from "./js/services/UnitsService";

export default function App() {
    const [authed, setAuthed] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");

    const [units, setUnits] = useState([]);
    const [toastMessage, setToastMessage] = useState("");
    const [showToast, setShowToast] = useState(false);

    useEffect(() => {
        const unitsService = new UnitsService();

        unitsService.getUnits().then((res) => {
            setUnits(res);
            setAuthed(true);
        }).catch(error => {
            setUnits([]);
            setAuthed(false);
        })
    }, []);

    return (
        <AuthedContext.Provider value={{authed, setAuthed}}>
            <AlertContext.Provider value={{alertMessage, setAlertMessage}}>
                <UnitsContext.Provider value={{units}}>
                    <div className="app">
                        <Nav />
                        <div className="container">
                            <Outlet context={[setToastMessage, setShowToast]} />

                            <Alert />
                            <AppToast message={toastMessage} show={showToast} setShow={setShowToast} />
                        </div>
                    </div>
                </UnitsContext.Provider>
            </AlertContext.Provider>
        </AuthedContext.Provider> 
    );
}
