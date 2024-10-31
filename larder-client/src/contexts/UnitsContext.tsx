import { createContext, Dispatch, SetStateAction } from "react";

import { Unit } from "../types/Unit";

interface UnitsContextType {
    units: Unit[];
    setUnits: Dispatch<SetStateAction<Unit[]>>;
}

export const UnitsContext = createContext<UnitsContextType>({
    units: [],
    setUnits: () => {}
});
