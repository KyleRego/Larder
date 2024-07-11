import "./App.css";
import Nav from "./Nav";
import { Outlet } from "react-router-dom";

export default function App()
{
  return (
    <div className="app">
      <Nav />
      <div>
        <Outlet />
      </div>
    </div>
  );
}
