export class UnitConversionDto {
    id: string | null;
    unitId: string;
    targetUnitsPerUnit: number;
    targetUnitId: string;

    constructor(id: string, unitId: string,
                targetUnitsPerUnit: number, targetUnitId: string) {
        this.id = id;
        this.unitId = unitId;
        this.targetUnitsPerUnit = targetUnitsPerUnit;
        this.targetUnitId = targetUnitId;
    }
}
