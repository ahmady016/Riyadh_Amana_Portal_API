using DB.Entities;
using DB.Common;
using Auth.Dtos;
using Dtos;

namespace Users;

public interface IUserService
{
    #region private methods
    /*
    RefreshToken GetRefreshToken(string token);
    void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason);
    void RevokeAllUserRefreshTokens(Guid userId, string ipAddress);
    void RemoveOldRefreshTokens(Guid userId);
    Tuple<string, RefreshToken> GenerateTokens(User user, string ipAddress = null);
    */
    #endregion

    AuthDto Register(RegisterInput input, string ipAddress);
    AuthDto Login(LoginInput login, string ipAddress);
    AuthDto RefreshTheTokens(string token, string ipAddress);
    bool RevokeTheToken(string token, string ipAddress);
    bool ChangePassword(ChangePasswordInput input);
    bool ChangeEmail(ChangeEmailInput input);
    bool Logout(Guid userId);

    UserDto Update(UpdateUserInput input);
    List<UserDto> UpdateMany(List<UpdateUserInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);

    List<UserDto> List(string type);
    PageResult<UserDto> ListPage(string type, int pageSize, int pageNumber);
    UserDto Find(Guid id);
    List<UserDto> FindList(string ids);
}
