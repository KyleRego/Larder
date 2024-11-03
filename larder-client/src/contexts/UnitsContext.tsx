import { createContext, Dispatch, SetStateAction } from "react";

import { UnitDto } from "../types/UnitDto";

interface UnitsContextType {
    units: UnitDto[];
    setUnits: Dispatch<SetStateAction<UnitDto[]>>;
}

export const UnitsContext = createContext<UnitsContextType>({
    units: [],
    setUnits: () => {}
});
