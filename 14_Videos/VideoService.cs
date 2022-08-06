using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Videos;

public class VideoService : IVideoService
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
            _errorMessage = $"Video Record with Id: {id} Not Found";
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
            _errorMessage = $"No Any Videos Records Found";
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
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<VideoDto>>(list);
    }

    public VideoDto Add(CreateVideoInput input)
    {
        // check if any titles are existed in db
        var oldVideo = _crudService.GetOne<Video>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        // if any titles existed then reject the input
        if (oldVideo is not null)
        {
            _errorMessage = $"Video: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if not do the normal Add action
        var video = _mapper.Map<Video>(input);
        var createdvideo = _crudService.Add<Video, Guid>(video);
        _crudService.SaveChanges();

        return _mapper.Map<VideoDto>(createdvideo);
    }
    public List<VideoDto> AddMany(List<CreateVideoInput> inputs)
    {
        // get all inputs new titles
        var titlesArList = inputs.Select(e => e.TitleAr).ToList();
        var titlesEnList = inputs.Select(e => e.TitleEn).ToList();

        // check if any titles aleary exist in db
        var existedVideos = _crudService.GetList<Video, Guid>(e => titlesArList.Contains(e.TitleAr) || titlesEnList.Contains(e.TitleEn));
        // if any new title aleary existed so reject all inputs
        if (existedVideos.Count > 0)
        {
            _errorMessage = $"Videos List was rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if all inputs titles are not existed in db then do the normal add many action
        var videos = _mapper.Map<List<Video>>(inputs);
        var createdvideos = _crudService.AddAndGetRange<Video, Guid>(videos);
        _crudService.SaveChanges();

        return _mapper.Map<List<VideoDto>>(createdvideos);
    }

    public VideoDto Update(UpdateVideoInput input)
    {
        // get old db item
        var oldVideo = GetById(input.Id);

        // if any titles changed
        if (oldVideo.TitleAr != input.TitleAr || oldVideo.TitleEn != input.TitleEn)
        {
            // check for its existance in db
            var CityExisted = _crudService.GetOne<Video>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            // if existed reject the update input
            if (CityExisted is not null)
            {
                _errorMessage = $"Video: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // if no titles changed or the changed ones not existed in db do the normal update
        var newVideo = _mapper.Map<Video>(input);

        FillRestPropsWithOldValues(oldVideo, newVideo);
        var updatedVideo = _crudService.Update<Video, Guid>(newVideo);
        _crudService.SaveChanges();

        return _mapper.Map<VideoDto>(updatedVideo);
    }
    public List<VideoDto> UpdateMany(List<UpdateVideoInput> inputs)
    {
        // get oldVideos List from db
        var oldVideos = GetByIds(inputs.Select(x => x.Id).ToList());

        // get inputsTitles and oldVideosTitles
        var inputsTitlesAr = inputs.Select(e => e.TitleAr);
        var inputsTitlesEn = inputs.Select(e => e.TitleEn);
        var videosTitlesAr = oldVideos.Select(e => e.TitleAr);
        var videosTitlesEn = oldVideos.Select(e => e.TitleEn);

        // get changedVideosTitles
        var changedVideosTitlesAr = inputsTitlesAr
            .Where(x => !videosTitlesAr.Contains(x))
            .ToList();
        var changedVideosTitlesEn = inputsTitlesEn
            .Where(x => !videosTitlesEn.Contains(x))
            .ToList();

        // if any titles changed check if aleary existed in db
        if (changedVideosTitlesAr.Count > 0 || changedVideosTitlesEn.Count > 0)
        {
            var existedVideos = _crudService.GetList<City, Guid>(e => changedVideosTitlesAr.Contains(e.TitleAr) || changedVideosTitlesEn.Contains(e.TitleEn));
            // if any existance found in db reject all inputs
            if (existedVideos.Count > 0)
            {
                _errorMessage = $"Videos List was rejected, Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // do the normal update many items action
        var newVideos = _mapper.Map<List<Video>>(inputs);

        for (int i = 0; i < oldVideos.Count; i++)
            FillRestPropsWithOldValues(oldVideos[i], newVideos[i]);
        var updatedVideos = _crudService.UpdateAndGetRange<Video, Guid>(newVideos);
        _crudService.SaveChanges();

        return _mapper.Map<List<VideoDto>>(updatedVideos);
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
