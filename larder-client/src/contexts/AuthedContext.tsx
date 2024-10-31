import { createContext, Dispatch, SetStateAction } from "react";

interface AuthedContextType {
    authed: boolean;
    setAuthed: Dispatch<SetStateAction<boolean>>;
}

export const AuthedContext = createContext<AuthedContextType>({
    authed: false,
    setAuthed: () => {}
});
