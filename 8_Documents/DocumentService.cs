using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Documents;

public class DocumentService : IDocumentService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Document> _logger;
    private string _errorMessage;

    public DocumentService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Document> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    public List<DocumentDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Document, Guid>(),
            "deleted" => _crudService.GetList<Document, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Document, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<DocumentDto>>(list);
    }
    public PageResult<DocumentDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Document>(),
            "deleted" => _crudService.GetQuery<Document>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Document>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<DocumentDto>()
        {
            PageItems = _mapper.Map<List<DocumentDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public DocumentDto Find(Guid id)
    {
        var user = _crudService.Find<Document, Guid>(id);
        if (user == null)
        {
            _errorMessage = $"Document Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        return _mapper.Map<DocumentDto>(user);
    }
    public List<DocumentDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Document: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').ToList();
        var list = _crudService.GetList<Document, Guid>(e => _ids.Contains(e.Id.ToString()));
        return _mapper.Map<List<DocumentDto>>(list);
    }

    public DocumentDto Add(CreateDocumentInput input)
    {
        var document = _mapper.Map<Document>(input);
        var createdDocument = _crudService.Add<Document, Guid>(document);
        _crudService.SaveChanges();
        return _mapper.Map<DocumentDto>(createdDocument);
    }
    public List<DocumentDto> AddMany(List<CreateDocumentInput> inputs)
    {
        var documents = _mapper.Map<List<Document>>(inputs);
        var createdDocuments = _crudService.AddAndGetRange<Document, Guid>(documents);
        _crudService.SaveChanges();
        return _mapper.Map<List<DocumentDto>>(createdDocuments);
    }

    public DocumentDto Update(UpdateDocumentInput input)
    {
        var document = _mapper.Map<Document>(input);
        var updatedDocument = _crudService.Update<Document, Guid>(document);
        _crudService.SaveChanges();
        return _mapper.Map<DocumentDto>(updatedDocument);
    }
    public List<DocumentDto> UpdateMany(List<UpdateDocumentInput> inputs)
    {
        var documents = _mapper.Map<List<Document>>(inputs);
        var updatedDocuments = _crudService.UpdateAndGetRange<Document, Guid>(documents);
        _crudService.SaveChanges();
        return _mapper.Map<List<DocumentDto>>(updatedDocuments);
    }

    public bool Delete(Guid id)
    {
        var document = _crudService.Find<Document, Guid>(id);
        if (document is not null)
        {
            _crudService.SoftDelete<Document, Guid>(document);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"Document record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
    public bool Activate(Guid id)
    {
        var document = _crudService.Find<Document, Guid>(id);
        if (document is not null)
        {
            _crudService.Activate<Document, Guid>(document);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"Document record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
}
