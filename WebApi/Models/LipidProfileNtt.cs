namespace Larder.Models;

public class LipidProfileNtt : LabResultNtt
{
    public double CholesterolTotal_mgPerdL { get; set; }
    public double Triglycerides_mgPerdL { get; set; }
    public double HDLCholesterol_mgPerdL { get; set; }
    public double VLDL_mgPerdL { get; set; }
    public double LDL_mgPerdL { get; set; }
}