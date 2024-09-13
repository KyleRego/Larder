import { createContext } from "react";

export const AuthedContext = createContext({ authed: null, setAuthed: () => {} });