import { createContext,
        Dispatch,
        ReactNode,
        SetStateAction,
        useState } from "react";

import { Unit } from "../types/Unit";

interface UnitsContextType {
    units: Unit[];
    setUnits: Dispatch<SetStateAction<Unit[]>>;
}

export const UnitsContext = createContext<UnitsContextType>({
    units: [],
    setUnits: () => {}
});

export const UnitsProvider = ({ children }: { children: ReactNode }) => {
    const [units, setUnits] = useState<Unit[]>([]);

    return (
        <UnitsContext.Provider value={{ units, setUnits }}>
            {children}
        </UnitsContext.Provider>
    );
};
