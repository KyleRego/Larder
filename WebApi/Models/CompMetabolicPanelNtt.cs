namespace Larder.Models;

public class CompMetabolicPanelNtt : LabResultNtt
{
    public double Glucose_mgPerdL { get; set; }
    public double BUN_mgPerdL { get; set; }
    public double Creatinine_mgPerdL { get; set; }
    // mL/min/1.73m2
    public double EGFR_mLPerminPerm2 { get; set; }
    public double BUNCreatinineRatio { get; set; }
    public double Sodium_mmolPerL { get; set; }
    public double Potassium_mmolPerL { get; set; }
    public double Chloride_mmolPerL { get; set; }
    public double CarbonDioxide_mmolPerL { get; set; }
    public double Calcium_mgPerdL { get; set; }
    public double ProteinTotal_gPerdL { get; set; }
    public double Albumin_gPerdL { get; set; }
    public double Globulin_gPerddL { get; set; }
    public double BilirubinTotal_mgPerdL { get; set; }
    public double AlkalinePhosphatase_IUPerL { get; set; }
    public double AST_IUPerL { get; set; }
    public double ALT_IUPerL { get; set; }

    public double HemoglobinA1c_Percent { get; set; }
}
