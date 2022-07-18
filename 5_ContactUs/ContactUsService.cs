
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace _ContactUs;

public class ContactUsService : IContactUsService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<ContactUs> _logger;
    private string _errorMessage;

    public ContactUsService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<ContactUs> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    public List<ContactUsDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<ContactUs, Guid>(),
            "deleted" => _crudService.GetList<ContactUs, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<ContactUs, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<ContactUsDto>>(list);
    }
    public PageResult<ContactUsDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<ContactUs>(),
            "deleted" => _crudService.GetQuery<ContactUs>(e => e.IsDeleted),
            _ => _crudService.GetQuery<ContactUs>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<ContactUsDto>()
        {
            PageItems = _mapper.Map<List<ContactUsDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public ContactUsDto Find(Guid id)
    {
        var news = _crudService.Find<ContactUs, Guid>(id);
        if (news == null)
        {
            _errorMessage = $"News Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        return _mapper.Map<ContactUsDto>(news);
    }
    public List<ContactUsDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"News: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }

        var _ids = ids.SplitAndRemoveEmpty(',').ToList();
        var list = _crudService.GetList<ContactUs, Guid>(e => _ids.Contains(e.Id.ToString()));
        return _mapper.Map<List<ContactUsDto>>(list);
    }

    public ContactUsDto Add(CreateContactUsInput input)
    {
        var contactUsItem = _mapper.Map<ContactUs>(input);
        var createdContactUsItem = _crudService.Add<ContactUs, Guid>(contactUsItem);
        _crudService.SaveChanges();
        return _mapper.Map<ContactUsDto>(createdContactUsItem);
    }
    public List<ContactUsDto> AddMany(List<CreateContactUsInput> inputs)
    {
        var contactUsList = _mapper.Map<List<ContactUs>>(inputs);
        var createdContactUsList = _crudService.AddAndGetRange<ContactUs, Guid>(contactUsList);
        _crudService.SaveChanges();
        return _mapper.Map<List<ContactUsDto>>(createdContactUsList);
    }

    public ContactUsDto Update(UpdateContactUsInput input)
    {
        var contactUsItem = _mapper.Map<ContactUs>(input);
        var updatedContactUsItem = _crudService.Update<ContactUs, Guid>(contactUsItem);
        _crudService.SaveChanges();
        return _mapper.Map<ContactUsDto>(updatedContactUsItem);
    }
    public List<ContactUsDto> UpdateMany(List<UpdateContactUsInput> inputs)
    {
        var contactUsList = _mapper.Map<List<ContactUs>>(inputs);
        var updatedContactUsList = _crudService.UpdateAndGetRange<ContactUs, Guid>(contactUsList);
        _crudService.SaveChanges();
        return _mapper.Map<List<ContactUsDto>>(updatedContactUsList);
    }

    public bool Delete(Guid id)
    {
        var contactUsItem = _crudService.Find<ContactUs, Guid>(id);
        if (contactUsItem is not null)
        {
            _crudService.SoftDelete<ContactUs, Guid>(contactUsItem);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"News record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
    public bool Activate(Guid id)
    {
        var contactUsItem = _crudService.Find<ContactUs, Guid>(id);
        if (contactUsItem is not null)
        {
            _crudService.Activate<ContactUs, Guid>(ncontactUsItemews);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"News record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
}
