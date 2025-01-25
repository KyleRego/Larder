import { Outlet } from 'react-router';
import { useEffect, useState } from 'react';
import { UnitsContext } from './contexts/UnitsContext';
import { apiClient } from './util/axios';
import { UnitDto } from './types/UnitDto';
import { AuthedContext } from './contexts/AuthedContext';
import { NavBar } from './NavBar';
import { MessageContext } from './contexts/MessageContext';
import MessageDisplay from './components/MessageDisplay';
import { Message } from './types/Message';

function App() {
    const [authed, setAuthed] = useState(false);
    const [units, setUnits] = useState<UnitDto[]>([]);
    const [message, setMessage] = useState<Message | null>(null)

    useEffect(() => {
        apiClient.get<UnitDto[]>("/api/units").then(res => {
            setUnits(res.data);
            setAuthed(true);
        }).catch(error => {
            if (error.response.status === 401) {
                console.log("GET units: user is unauthenticated.")
            } else {
                console.error("An unexpected error occured", error);
            }
        })
    }, [setUnits]);

    return (
        <AuthedContext.Provider value={{authed, setAuthed}}>
            <UnitsContext.Provider value={{units, setUnits}}>
                <MessageContext.Provider value={{message, setMessage}}>
                    <div className="bg-secondary min-vh-100 d-flex flex-column">
                        <NavBar />
                        <div className="container-fluid flex-grow-1 h-100 d-flex flex-column justify-content-center">
                            <div className="flex-grow-1 m-4 mb-5 card shadow-sm">
                                <div className="card-body">
                                    <Outlet />
                                </div>
                            </div>
                        </div>
                    </div>
                    <MessageDisplay />
                </MessageContext.Provider>
            </UnitsContext.Provider>
        </AuthedContext.Provider>
    );
}

export default App
