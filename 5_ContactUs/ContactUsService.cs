
using AutoMapper;
using System.Net;

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

    private ContactUs GetById(Guid id)
    {
        var contactUsItem = _crudService.Find<ContactUs, Guid>(id);
        if (contactUsItem is null)
        {
            _errorMessage = $"ContactUs Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return contactUsItem;
    }
    private List<ContactUs> GetByIds(List<Guid> ids)
    {
        var contactUsList = _crudService.GetList<ContactUs, Guid>(e => ids.Contains(e.Id));
        if (contactUsList.Count == 0)
        {
            _errorMessage = $"No Any ContactUs Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return contactUsList;
    }
    private static void FillRestPropsWithOldValues(ContactUs oldItem, ContactUs newItem)
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
        var contactUsItem = GetById(id);
        return _mapper.Map<ContactUsDto>(contactUsItem);
    }
    public List<ContactUsDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"ContactUs: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
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
        var oldContactUsItem = GetById(input.Id);
        var newContactUsItem = _mapper.Map<ContactUs>(input);

        FillRestPropsWithOldValues(oldContactUsItem, newContactUsItem);
        var updatedContactUsItem = _crudService.Update<ContactUs, Guid>(newContactUsItem);
        _crudService.SaveChanges();
        
        return _mapper.Map<ContactUsDto>(updatedContactUsItem);
    }
    public List<ContactUsDto> UpdateMany(List<UpdateContactUsInput> inputs)
    {
        var oldContactUsList = GetByIds(inputs.Select(x => x.Id).ToList());
        var newContactUsList = _mapper.Map<List<ContactUs>>(inputs);

        for (int i = 0; i < oldContactUsList.Count; i++)
            FillRestPropsWithOldValues(oldContactUsList[i], newContactUsList[i]);
        var updatedContactUsList = _crudService.UpdateAndGetRange<ContactUs, Guid>(newContactUsList);
        _crudService.SaveChanges();
        
        return _mapper.Map<List<ContactUsDto>>(updatedContactUsList);
    }

    public bool Delete(Guid id)
    {
        var contactUsItem = GetById(id);
        _crudService.SoftDelete<ContactUs, Guid>(contactUsItem);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var contactUsItem = GetById(id);
        _crudService.Activate<ContactUs, Guid>(contactUsItem);
        _crudService.SaveChanges();
        return true;
    }

}
