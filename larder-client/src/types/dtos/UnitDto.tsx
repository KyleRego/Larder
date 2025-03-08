import { UnitConversionDto } from "./UnitConversionDto";
import { UnitType } from "./UnitType";

export class UnitDto {
    id: string | null;
    name: string;
    type: UnitType;
    conversions: UnitConversionDto[];

    constructor(id: string | null,
                name: string,
                type: UnitType,
                conversions: UnitConversionDto[]) {
        this.id = id;
        this.name = name;
        this.type = type;
        this.conversions = conversions;
    }

    public static getType(unit: UnitDto): string {
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
