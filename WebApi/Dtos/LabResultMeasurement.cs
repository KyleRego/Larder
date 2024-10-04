namespace Larder.Dtos;

public class LabResultMeasurement(string name, double value, string unit)
{
    public string Name { get; set; } = name;

    public double Value { get; set; } = value;

    public string Unit { get; set; } = unit;
}
