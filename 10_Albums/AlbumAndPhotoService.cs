using System.Net;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Albums;

public class AlbumAndPhotoService : IAlbumAndPhotoService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Album> _logger;
    private string _errorMessage;

    public AlbumAndPhotoService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Album> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private Album GetAlbumById(Guid id)
    {
        var album = _crudService.Find<Album, Guid>(id);
        if (album is null)
        {
            _errorMessage = $"Album Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return album;
    }
    private List<Album> GetAlbumsByIds(List<Guid> ids)
    {
        var albums = _crudService.GetList<Album, Guid>(e => ids.Contains(e.Id));
        if (albums.Count == 0)
        {
            _errorMessage = $"No Any Album Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return albums;
    }
    private static void FillRestPropsWithOldValues(Album oldItem, Album newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }

    private Photo GetPhotoById(Guid id)
    {
        var photo = _crudService.Find<Photo, Guid>(id);
        if (photo is null)
        {
            _errorMessage = $"Photo Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return photo;
    }
    private List<Photo> GetPhotosByIds(List<Guid> ids)
    {
        var photos = _crudService.GetList<Photo, Guid>(e => ids.Contains(e.Id));
        if (photos.Count == 0)
        {
            _errorMessage = $"No Any Photo Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return photos;
    }
    private static void FillRestPropsWithOldValues(Photo oldItem, Photo newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }
    #endregion

    public List<AlbumDto> ListAlbums(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Album, Guid>(),
            "deleted" => _crudService.GetList<Album, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Album, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<AlbumDto>>(list);
    }
    public PageResult<AlbumDto> ListAlbumsPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Album>(),
            "deleted" => _crudService.GetQuery<Album>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Album>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<AlbumDto>()
        {
            PageItems = _mapper.Map<List<AlbumDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public AlbumDto FindOneAlbum(Guid id)
    {
        var album = GetAlbumById(id);
        return _mapper.Map<AlbumDto>(album);
    }
    public List<AlbumDto> FindManyAlbums(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Album: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetAlbumsByIds(_ids);
        return _mapper.Map<List<AlbumDto>>(list);
    }

    public AlbumDto AddAlbum(CreateAlbumInput input)
    {
        var album = _mapper.Map<Album>(input);
        var createdAlbum = _crudService.Add<Album, Guid>(album);
        _crudService.SaveChanges();
        return _mapper.Map<AlbumDto>(createdAlbum);
    }
    public AlbumDto AddAlbumWithPhotos(CreateAlbumWithPhotosInput input)
    {
        var album = _mapper.Map<Album>(input);
        var createdAlbum = _crudService.Add<Album, Guid>(album);
        _crudService.SaveChanges();
        return _mapper.Map<AlbumDto>(createdAlbum);
    }
    public List<AlbumDto> AddManyAlbums(List<CreateAlbumInput> inputs)
    {
        var albums = _mapper.Map<List<Album>>(inputs);
        var createdAlbums = _crudService.AddAndGetRange<Album, Guid>(albums);
        _crudService.SaveChanges();
        return _mapper.Map<List<AlbumDto>>(createdAlbums);
    }

    public AlbumDto UpdateAlbum(UpdateAlbumInput input)
    {
        var oldAlbum = GetAlbumById(input.Id);
        var newAlbum = _mapper.Map<Album>(input);

        FillRestPropsWithOldValues(oldAlbum, newAlbum);
        var updatedAlbum = _crudService.Update<Album, Guid>(newAlbum);
        _crudService.SaveChanges();

        return _mapper.Map<AlbumDto>(updatedAlbum);
    }
    public List<AlbumDto> UpdateManyAlbums(List<UpdateAlbumInput> inputs)
    {
        var oldAlbums = GetAlbumsByIds(inputs.Select(x => x.Id).ToList());
        var newAlbums = _mapper.Map<List<Album>>(inputs);

        for (int i = 0; i < oldAlbums.Count; i++)
            FillRestPropsWithOldValues(oldAlbums[i], newAlbums[i]);
        var updatedAlbums = _crudService.UpdateAndGetRange<Album, Guid>(newAlbums);
        _crudService.SaveChanges();

        return _mapper.Map<List<AlbumDto>>(updatedAlbums);
    }

    public bool DeleteAlbum(Guid id)
    {
        var album = GetAlbumById(id);
        _crudService.SoftDelete<Album, Guid>(album);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateAlbum(Guid id)
    {
        var album = GetAlbumById(id);
        _crudService.Activate<Album, Guid>(album);
        _crudService.SaveChanges();
        return true;
    }

    public List<PhotoDto> ListPhotos(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Photo, Guid>(),
            "deleted" => _crudService.GetList<Photo, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Photo, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<PhotoDto>>(list);
    }
    public PageResult<PhotoDto> ListPhotosPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Photo>(),
            "deleted" => _crudService.GetQuery<Photo>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Photo>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<PhotoDto>()
        {
            PageItems = _mapper.Map<List<PhotoDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public PhotoDto FindOnePhoto(Guid id)
    {
        var photo = GetPhotoById(id);
        return _mapper.Map<PhotoDto>(photo);
    }
    public List<PhotoDto> FindManyPhotos(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Photo: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetPhotosByIds(_ids);
        return _mapper.Map<List<PhotoDto>>(list);
    }

    public PhotoDto AddPhoto(CreatePhotoInput input)
    {
        var photo = _mapper.Map<Photo>(input);
        var createdPhoto = _crudService.Add<Photo, Guid>(photo);
        _crudService.SaveChanges();
        return _mapper.Map<PhotoDto>(createdPhoto);
    }
    public List<PhotoDto> AddManyPhotos(List<CreatePhotoInput> inputs)
    {
        var photos = _mapper.Map<List<Photo>>(inputs);
        var createdPhotos = _crudService.AddAndGetRange<Photo, Guid>(photos);
        _crudService.SaveChanges();
        return _mapper.Map<List<PhotoDto>>(createdPhotos);
    }

    public PhotoDto UpdatePhoto(UpdatePhotoInput input)
    {
        var oldPhoto = GetPhotoById(input.Id);
        var newPhoto = _mapper.Map<Photo>(input);

        FillRestPropsWithOldValues(oldPhoto, newPhoto);
        var updatedPhoto = _crudService.Update<Photo, Guid>(newPhoto);
        _crudService.SaveChanges();

        return _mapper.Map<PhotoDto>(updatedPhoto);
    }
    public List<PhotoDto> UpdateManyPhotos(List<UpdatePhotoInput> inputs)
    {
        var oldPhotos = GetPhotosByIds(inputs.Select(x => x.Id).ToList());
        var newPhotos = _mapper.Map<List<Photo>>(inputs);

        for (int i = 0; i < oldPhotos.Count; i++)
            FillRestPropsWithOldValues(oldPhotos[i], newPhotos[i]);
        var updatedPhotos = _crudService.UpdateAndGetRange<Photo, Guid>(newPhotos);
        _crudService.SaveChanges();

        return _mapper.Map<List<PhotoDto>>(updatedPhotos);
    }
    
    public bool DeletePhoto(Guid id)
    {
        var photo = GetPhotoById(id);
        _crudService.SoftDelete<Photo, Guid>(photo);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivatePhoto(Guid id)
    {
        var photo = GetPhotoById(id);
        _crudService.Activate<Photo, Guid>(photo);
        _crudService.SaveChanges();
        return true;
    }

}
