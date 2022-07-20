using DB.Common;
using Dtos;

namespace Documents;

public interface IDocumentService
{
    List<DocumentDto> List(string type);
    PageResult<DocumentDto> ListPage(string type, int pageSize, int pageNumber);
    DocumentDto Find(Guid id);
    List<DocumentDto> FindList(string ids);
    DocumentDto Add(CreateDocumentInput input);
    List<DocumentDto> AddMany(List<CreateDocumentInput> inputs);
    DocumentDto Update(UpdateDocumentInput input);
    List<DocumentDto> UpdateMany(List<UpdateDocumentInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
