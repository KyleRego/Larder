namespace Larder.Dtos;

public class FieldMeta
{
    public required string Name       { get; set; }
    public required string DataType   { get; set; }
    public bool   IsRequired { get; set; }
}