namespace Larder.Dtos;

public class ItemImageDto
{
    public required byte[] ImageData { get; set; }
    public required string ImageContentType { get; set; }
}