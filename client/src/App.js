/*  Larder is TODO
    Copyright (C) 2024  Kyle Rego

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>. */

import { useState } from 'react';
import './App.css';
import Units from './Units';
import Conversions from './Conversions';
import Recipes from './Recipes';

export default function App()
{
  const [currentTab, setCurrentTab] = useState(false);

  const handleChooseUnits = () => { setCurrentTab("units"); };
  const handleChooseConversions = () => { setCurrentTab("conversions"); };
  const handleChooseRecipes = () => { setCurrentTab("recipes") };

  return (
    <div className="app">
      <header className="tabSelector">
        <span className="tabOption" onClick={handleChooseUnits}>
          Units
        </span>
        <span className="tabOption" onClick={handleChooseConversions}>
          Conversions
        </span>
        <span className="tabOption" onClick={handleChooseRecipes}>
          Recipes
        </span>
        <span className="tabOption">
          Ingredients
        </span>
        <span className="tabOption">
          Premades
        </span>
        <span className="tabOption">
          Notes
        </span>
      </header>
      <div className="currentTab">
        {(currentTab === "units") && <Units />}
        {(currentTab === "conversions") && <Conversions />}
        {(currentTab === "recipes") && <Recipes /> }
      </div>
    </div>
  );
}
