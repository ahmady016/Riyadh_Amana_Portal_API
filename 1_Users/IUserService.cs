using DB.Common;
using Dtos;

namespace Users;

public interface IUserService
{
    List<UserDto> List(string type);
    PageResult<UserDto> ListPage(string type, int pageSize, int pageNumber);
    UserDto Find(Guid id);
    List<UserDto> FindList(string ids);
    UserDto Add(CreateUserInput input);
    List<UserDto> AddMany(List<CreateUserInput> inputs);
    UserDto Update(UpdateUserInput input);
    List<UserDto> UpdateMany(List<UpdateUserInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
