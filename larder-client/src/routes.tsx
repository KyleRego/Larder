import { createBrowserRouter } from 'react-router-dom';
import App from './App';
import Index from './pages/Index';
import NewItem from './pages/NewItem';
import Items from './pages/Items';
import Login from './pages/Login';
import Register from './pages/Register';
import Units from './pages/Units';
import NewUnit from './pages/NewUnit';
import UnitPage from './pages/Unit';
import EditUnit from './pages/EditUnit';
import Item from './pages/Item';
import EditItem from './pages/EditItem';

export const router = createBrowserRouter([{
    path: "/",
    element: <App />,
    children: [
        { path: '/', element: <Index /> },
        { path: '/login', element: <Login /> },
        { path: '/register', element: <Register />},
        { path: '/items', element: <Items /> },
        { path: '/items/new', element: <NewItem /> },
        { path: '/items/:id', element: <Item /> },
        { path: '/items/:id/edit', element: <EditItem /> },
        { path: '/units', element: <Units /> },
        { path: '/units/:id', element: <UnitPage /> },
        { path: '/units/:id/edit', element: <EditUnit /> },
        { path: '/units/new', element: <NewUnit /> }
    ]
}]);
