import { createContext,
        Dispatch,
        ReactNode,
        SetStateAction,
        useState } from "react";

interface AuthedContextType {
    authed: boolean;
    setAuthed: Dispatch<SetStateAction<boolean>>;
}

export const AuthedContext = createContext<AuthedContextType>({
    authed: false,
    setAuthed: () => {}
});

export const AuthedProvider = ({ children }: { children: ReactNode }) => {
    const [authed, setAuthed] = useState<boolean>(false);

    return (
        <AuthedContext.Provider value={{ authed, setAuthed }}>
            {children}
        </AuthedContext.Provider>
    );
};
