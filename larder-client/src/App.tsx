import './App.css'
import { RouterProvider } from 'react-router'
import { router } from './routes'
import { useContext, useEffect } from 'react'
import { UnitsContext, UnitsProvider } from './contexts/UnitsContext'
import { apiClient } from './util/axios'
import { Unit } from './types/Unit'
import { AuthedContext, AuthedProvider } from './contexts/AuthedContext'

function App() {
    const { units, setUnits } = useContext(UnitsContext);
    const { authed, setAuthed } = useContext(AuthedContext);

    useEffect(() => {
        try {
            apiClient.get<Unit[]>("/api/units").then(res => {
                setUnits(res.data);
                setAuthed(true);
            });
        } catch (error) {
            console.error("Failed to fetch units:", error);
        }
    }, [setUnits]);

    return (
        <>
            <AuthedProvider>
                <UnitsProvider>
                    <div>
                        <RouterProvider router={router} />
                    </div>
                </UnitsProvider>
            </AuthedProvider>
        </>
    );
}

export default App
