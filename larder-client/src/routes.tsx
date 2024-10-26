import { createBrowserRouter } from 'react-router-dom';
import Index from './pages/Index';
import NewItem from './pages/NewItem';
import Items from './pages/Items';
import Login from './pages/Login';
import Register from './pages/Register';
import App from './App';

export const router = createBrowserRouter([
    {   path: "/",
        element: <App />,
        children:
            [   { path: '/', element: <Index /> },
                { path: '/items', element: <Items /> },
                { path: '/items/new', element: <NewItem /> },
                { path: '/login', element: <Login /> },
                { path: '/register', element: <Register />}
            ]
    }
]);
