import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Conversions from './Conversions';
import Units from "./Units";
import Recipes from "./Recipes";
import Recipe from "./Recipe";
import NewRecipe from "./NewRecipe";
import reportWebVitals from './reportWebVitals';

import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from "react-router-dom";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<App />}>
      <Route path="conversions" element={<Conversions />} />
      <Route path="units" element={<Units />} />
      <Route path="recipes" element={<Recipes />} />
      <Route path="recipes/:id" element = {<Recipe />} />
      <Route path="recipes/new" element = {<NewRecipe />} />
    </Route>
  )
)

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
