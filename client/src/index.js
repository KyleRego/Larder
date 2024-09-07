import "./bootstrap/bootstrap.css";

import React from 'react';
import ReactDOM from 'react-dom/client';

import App from './App';
import Home from "./js/Home";

import Units from "./js/units/Units";
import Unit from "./js/units/Unit";
import NewUnit from "./js/units/NewUnit";
import EditUnit from "./js/units/EditUnit";

import Foods from "./js/foods/Foods";
import Food from "./js/foods/Food";
import NewFood from './js/foods/NewFood';
import EditFood from './js/foods/EditFood';

import Ingredients from "./js/ingredients/Ingredients";
import Ingredient from "./js/ingredients/Ingredient";
import NewIngredient from "./js/ingredients/NewIngredient";
import EditIngredient from './js/ingredients/EditIngredient';

import Recipes from "./js/recipes/Recipes";
import Recipe from "./js/recipes/Recipe";
import NewRecipe from "./js/recipes/NewRecipe";
import EditRecipe from "./js/recipes/EditRecipe";

import reportWebVitals from './reportWebVitals';

import Timeline from "./js/timeline/Timeline";
import Register from "./js/identity/Register";
import Login from "./js/identity/Login";

import "./index.css";
import "./css/buttons.css";
import "./css/utility.css";
import "./css/tables.css";
import "./css/forms.css";
import "./css/cards.css";

import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from "react-router-dom";

import UnitsService from "./js/services/UnitsService";

const unitsService = new UnitsService();

const units = await unitsService.getUnits();

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<App />}>
      <Route path="" element={<Home />} />
      <Route path="register" element={<Register />} />
      <Route path="login" element={<Login />} />
      <Route path="timeline" element={<Timeline />} />
      <Route path="units" element={<Units />} />
      <Route path="units/:id" element={<Unit />} />
      <Route path="units/new" element={<NewUnit />} />
      <Route path="units/:id/edit" element={<EditUnit />} />
      <Route path="foods" element={<Foods units={units} />} />
      <Route path="foods/:id" element={<Food />} />
      <Route path="foods/new" element={<NewFood units={units} />} />
      <Route path="foods/:id/edit" element={<EditFood units={units} />} />
      <Route path="ingredients" element={<Ingredients units={units} />} />
      <Route path="ingredients/:id" element={<Ingredient units={units} />} />
      <Route path="ingredients/:id/edit" element={<EditIngredient units={units} />} />
      <Route path="ingredients/new" element={<NewIngredient units={units} />} />
      <Route path="recipes" element={<Recipes />} />
      <Route path="recipes/new" element = {<NewRecipe units={units} />} />
      <Route path="recipes/:id" element = {<Recipe units={units} />} />
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
