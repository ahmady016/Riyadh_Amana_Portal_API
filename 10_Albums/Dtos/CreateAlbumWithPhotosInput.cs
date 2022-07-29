namespace Dtos;

public class CreateAlbumWithPhotosInput : CreateAlbumInput
{
    public List<CreatePhotoInput> Photos { get; set; }
}
