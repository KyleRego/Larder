import { UnitType } from "./UnitType"

export class Unit {
    id: string | null;
    name: string;
    type: UnitType;

    constructor(id: string | null, name: string, type: UnitType) {
        this.id = id;
        this.name = name;
        this.type = type;
    }

    public static getType(unit: Unit): string {
        switch (unit.type) {
            case UnitType.Mass:
                return "Mass";
            case UnitType.Volume:
                return "Volume";
            case UnitType.Weight:
                return "Weight";
        }
    }
}