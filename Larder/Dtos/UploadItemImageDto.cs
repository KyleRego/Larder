namespace Larder.Dtos;

public class UploadItemImageDto
{
    public required string ItemId { get; set; }

    public required IFormFile ImageFile { get; set; }
}
