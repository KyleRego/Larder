import React from 'react';
import ReactDOM from 'react-dom/client';

import App from './App';
import Conversions from './Conversions';
import Units from "./units/Units";

import Ingredients from "./ingredients/Ingredients";
import Ingredient from "./ingredients/Ingredient";
import NewIngredient from "./ingredients/NewIngredient";

import Recipes from "./recipes/Recipes";
import Recipe from "./recipes/Recipe";
import NewRecipe from "./recipes/NewRecipe";
import EditRecipe from "./recipes/EditRecipe";

import reportWebVitals from './reportWebVitals';

import "./index.css";
import "./buttons.css";
import "./utility.css";
import "./tables.css";
import "./forms.css";

import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from "react-router-dom";

import UnitsService from "./services/UnitsService";
import EditIngredient from './ingredients/EditIngredient';

const unitsService = new UnitsService();

const units = await unitsService.getUnits();

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<App />}>
      <Route path="conversions" element={<Conversions />} />
      <Route path="units" element={<Units />} />
      <Route path="ingredients" element={<Ingredients />} />
      <Route path="ingredients/:id" element={<Ingredient />} />
      <Route path="ingredients/:id/edit" element={<EditIngredient units={units} />} />
      <Route path="ingredients/new" element={<NewIngredient units={units} />} />
      <Route path="recipes" element={<Recipes />} />
      <Route path="recipes/new" element = {<NewRecipe units={units} />} />
      <Route path="recipes/:id" element = {<Recipe />} />
      <Route path="recipes/:id/edit" element = {<EditRecipe units={units} />} />
      
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
