using Larder.Models;

namespace Larder.Dtos;

public class LabResult
{
    public string? Id { get; set; }

    public DateTime DateTime { get; set; }

    public List<LabResultMeasurement> Measurements { get; set; } = [];

    public static LabResult FromEntity(LabResultNtt entity)
    {
        LabResult result = new()
        {
            Id = entity.Id,
            DateTime = entity.DateTime
        };

        if (entity is CompMetabolicPanelNtt cmp)
        {
            result.Measurements.AddRange([
                new LabResultMeasurement("Albumin", cmp.Albumin_gPerdL, "g/dL"),
                new LabResultMeasurement("Alkaline Phosphatase", cmp.AlkalinePhosphatase_IUPerL, "IU/L"),
                new LabResultMeasurement("ALT", cmp.ALT_IUPerL, "IU/L"),
                new LabResultMeasurement("AST", cmp.AST_IUPerL, "IU/L"),
                new LabResultMeasurement("Bilirubin", cmp.BilirubinTotal_mgPerdL, "mg/dL"),
                new LabResultMeasurement("BUN", cmp.BUN_mgPerdL, "mg/dL"),
                new LabResultMeasurement("BUN Creatinine Ratio", cmp.BUNCreatinineRatio, ""),
                new LabResultMeasurement("Calcium", cmp.Calcium_mgPerdL, "mg/dL"),
                new LabResultMeasurement("Carbon Dioxide", cmp.CarbonDioxide_mmolPerL, "mmol/L"),
                new LabResultMeasurement("Chloride", cmp.Chloride_mmolPerL, "mmol/L"),
                new LabResultMeasurement("Creatinine", cmp.Creatinine_mgPerdL, "mg/L"),
                new LabResultMeasurement("", cmp.EGFR_mLPerminPerm2, "mL/min/1.73m2"),
                new LabResultMeasurement("")
            ]);
        }
        else if (entity is LipidProfileNtt lipProf)
        {
            result.Measurements.AddRange([
                new LabResultMeasurement("Cholesterol, Total", lipProf.CholesterolTotal_mgPerdL, "mg/dL"),
            ])
        }
    }
}
