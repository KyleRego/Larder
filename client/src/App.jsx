import { useState, useEffect } from "react"
import { Outlet } from "react-router-dom";
import { AuthedContext } from "./AuthedContext";
import { AlertContext } from "./AlertContext";
import { UnitsContext } from "./UnitsContext";
import "./App.css";
import Nav from "./Nav";
import Alert from "./Alert";
import UnitsService from "./js/services/UnitsService";

export default function App() {
    const [authed, setAuthed] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");

    const [units, setUnits] = useState([]);

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
                <UnitsContext.Provider value={{units, setUnits}}>
                    <div className="app" style={{width: "100%", maxWidth: "100vw"}}>
                        <Nav />
                        <div className="container">
                            <div className="card shadow-sm mt-4">
                                <div className="card-body">
                                    <Outlet />
                                </div>
                            </div>

                            <Alert />
                        </div>
                    </div>
                </UnitsContext.Provider>
            </AlertContext.Provider>
        </AuthedContext.Provider> 
    );
}
