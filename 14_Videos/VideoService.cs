using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;
using System.Net;

namespace Videos;

public class VideoService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Video> _logger;
    private string _errorMessage;

    public VideoService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Video> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private Video GetById(Guid id)
    {
        var video = _crudService.Find<Video, Guid>(id);
        if (video == null)
        {
            _errorMessage = $"Advertisement Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return video;
    }
    private List<Video> GetByIds(List<Guid> ids)
    {
        var list = _crudService.GetList<Video, Guid>(e => ids.Contains(e.Id));
        if (list.Count == 0)
        {
            _errorMessage = $"No Any Advertisements Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return list;
    }
    private static void FillRestPropsWithOldValues(Video oldItem, Video newItem)
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

    public List<VideoDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Video, Guid>(),
            "deleted" => _crudService.GetList<Video, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Video, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<VideoDto>>(list);
    }
    public PageResult<VideoDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Video>(),
            "deleted" => _crudService.GetQuery<Video>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Video>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<VideoDto>()
        {
            PageItems = _mapper.Map<List<VideoDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public VideoDto Find(Guid id)
    {
        var video = GetById(id);
        return _mapper.Map<VideoDto>(video);
    }
    public List<VideoDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Video: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<VideoDto>>(list);
    }

    public VideoDto Add(CreateVideoInput input)
    {
        var video = _mapper.Map<Video>(input);
        var createdvideo = _crudService.Add<Video, Guid>(video);
        _crudService.SaveChanges();
        return _mapper.Map<VideoDto>(createdvideo);
    }
    public List<VideoDto> AddMany(List<CreateVideoInput> inputs)
    {
        var videos = _mapper.Map<List<Video>>(inputs);
        var createdvideos = _crudService.AddAndGetRange<Video, Guid>(videos);
        _crudService.SaveChanges();
        return _mapper.Map<List<VideoDto>>(createdvideos);
    }

    public VideoDto Update(UpdateVideoInput input)
    {
        var oldvideo = GetById(input.Id);
        var newvideo = _mapper.Map<Video>(input);

        FillRestPropsWithOldValues(oldvideo, newvideo);
        var updatedAdvertisement = _crudService.Update<Video, Guid>(newvideo);
        _crudService.SaveChanges();

        return _mapper.Map<VideoDto>(updatedAdvertisement);
    }
    public List<VideoDto> UpdateMany(List<UpdateVideoInput> inputs)
    {
        var oldvideos = GetByIds(inputs.Select(x => x.Id).ToList());
        var newvideos = _mapper.Map<List<Video>>(inputs);

        for (int i = 0; i < oldvideos.Count; i++)
            FillRestPropsWithOldValues(oldvideos[i], newvideos[i]);
        var updatedvideos = _crudService.UpdateAndGetRange<Video, Guid>(newvideos);
        _crudService.SaveChanges();

        return _mapper.Map<List<VideoDto>>(updatedvideos);
    }

    public bool Delete(Guid id)
    {
        var video = GetById(id);
        _crudService.SoftDelete<Video, Guid>(video);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var video = GetById(id);
        _crudService.Activate<Video, Guid>(video);
        _crudService.SaveChanges();
        return true;
    }
}
