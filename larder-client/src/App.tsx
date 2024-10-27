import { Outlet } from 'react-router';
import { useContext, useEffect } from 'react';
import { UnitsContext, UnitsProvider } from './contexts/UnitsContext';
import { apiClient } from './util/axios';
import { Unit } from './types/Unit';
import { AuthedContext, AuthedProvider } from './contexts/AuthedContext';
import { NavBar } from './NavBar';

function App() {
    const { setUnits } = useContext(UnitsContext);
    const { setAuthed } = useContext(AuthedContext);

    useEffect(() => {
        apiClient.get<Unit[]>("/api/units").then(res => {
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
        <>
            <AuthedProvider>
                <UnitsProvider>
                    <div className="bg-secondary min-vh-100">
                        <NavBar />
                        <div className="container d-flex flex-column justify-content-center">
                            <div className="card shadow-sm mt-4">
                                <div className="card-body">
                                    <Outlet />
                                </div>
                            </div>
                        </div>
                    </div>
                </UnitsProvider>
            </AuthedProvider>
        </>
    );
}

export default App
