using Dtos;
using DB.Common;

namespace Albums;

public interface IAlbumAndPhotoService
{
    List<AlbumDto> ListAlbums(string type);
    PageResult<AlbumDto> ListAlbumsPage(string type, int pageSize, int pageNumber);
    AlbumDto FindOneAlbum(Guid id);
    List<AlbumDto> FindManyAlbums(string ids);
    AlbumDto AddAlbum(CreateAlbumInput input);
    List<AlbumDto> AddManyAlbums(List<CreateAlbumInput> inputs);
    AlbumDto AddAlbumWithPhotos(CreateAlbumWithPhotosInput input);
    AlbumDto UpdateAlbum(UpdateAlbumInput input);
    List<AlbumDto> UpdateManyAlbums(List<UpdateAlbumInput> inputs);
    bool DeleteAlbum(Guid id);
    bool ActivateAlbum(Guid id);

    List<PhotoDto> ListPhotos(string type);
    PageResult<PhotoDto> ListPhotosPage(string type, int pageSize, int pageNumber);
    PhotoDto FindOnePhoto(Guid id);
    List<PhotoDto> FindManyPhotos(string ids);
    PhotoDto AddPhoto(CreatePhotoInput input);
    List<PhotoDto> AddManyPhotos(List<CreatePhotoInput> inputs);
    PhotoDto UpdatePhoto(UpdatePhotoInput input);
    List<PhotoDto> UpdateManyPhotos(List<UpdatePhotoInput> inputs);
    bool DeletePhoto(Guid id);
    bool ActivatePhoto(Guid id);
}
