using System.Net;
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

    private Document GetById(Guid id)
    {
        var document = _crudService.Find<Document, Guid>(id);
        if (document is null)
        {
            _errorMessage = $"Document Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return document;
    }
    private List<Document> GetByIds(List<Guid> ids)
    {
        var documents = _crudService.GetList<Document, Guid>(e => ids.Contains(e.Id));
        if (documents.Count == 0)
        {
            _errorMessage = $"No Any Document Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return documents;
    }
    private static void FillRestPropsWithOldValues(Document oldItem, Document newItem)
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
        var user = GetById(id);
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
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
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
        var oldDocument = GetById(input.Id);
        var newDocument = _mapper.Map<Document>(input);

        FillRestPropsWithOldValues(oldDocument, newDocument);
        var updatedDocument = _crudService.Update<Document, Guid>(newDocument);
        _crudService.SaveChanges();

        return _mapper.Map<DocumentDto>(updatedDocument);
    }
    public List<DocumentDto> UpdateMany(List<UpdateDocumentInput> inputs)
    {
        var oldDocuments = GetByIds(inputs.Select(x => x.Id).ToList());
        var newDocuments = _mapper.Map<List<Document>>(inputs);

        for (int i = 0; i < oldDocuments.Count; i++)
            FillRestPropsWithOldValues(oldDocuments[i], newDocuments[i]);
        var updatedDocuments = _crudService.UpdateAndGetRange<Document, Guid>(newDocuments);
        _crudService.SaveChanges();

        return _mapper.Map<List<DocumentDto>>(updatedDocuments);
    }

    public bool Delete(Guid id)
    {
        var document = GetById(id);
        _crudService.SoftDelete<Document, Guid>(document);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var document = GetById(id);
        _crudService.Activate<Document, Guid>(document);
        _crudService.SaveChanges();
        return true;
    }

}
