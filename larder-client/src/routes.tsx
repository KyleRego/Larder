import { createBrowserRouter } from 'react-router-dom';
import App from './components/layout/App';
import Index from './components/pages/Index';
import NewItem from './components/pages/NewItem';
import ItemsTables from './components/pages/Items';
import Login from './components/pages/Login';
import Register from './components/pages/Register';
import Units from './components/pages/Units';
import NewUnit from './components/pages/NewUnit';
import UnitPage from './components/pages/Unit';
import EditUnit from './components/pages/EditUnit';
import Item from './components/pages/Item';
import EditItem from './components/pages/EditItem';
import EatFood from './components/pages/EatFood';
import FoodLog from './components/pages/FoodLog';
import ItemsGrid from './components/pages/ItemsGrid';
import { EditRecipe } from './components/pages/EditRecipe';
import NewRecipe from './components/pages/NewRecipe';

export const router = createBrowserRouter([{
    path: "/",
    element: <App />,
    children: [
        { path: '/', element: <Index /> },
        { path: '/login', element: <Login /> },
        { path: '/register', element: <Register />},
        { path: '/items', element: <ItemsTables /> },
        { path: '/items/new', element: <NewItem /> },
        { path: '/items/:id', element: <Item /> },
        { path: '/items/:id/edit', element: <EditItem /> },
        { path: '/items/:id/eat', element: <EatFood /> },
        { path: '/units', element: <Units /> },
        { path: '/units/:id', element: <UnitPage /> },
        { path: '/units/:id/edit', element: <EditUnit /> },
        { path: '/units/new', element: <NewUnit /> },
        { path: '/food-log', element: <FoodLog /> },
        { path: '/items-grid', element: <ItemsGrid /> },
        { path: '/recipes/:id/edit', element: <EditRecipe /> },
        { path: '/recipes/new', element: <NewRecipe /> }
    ]
}]);
