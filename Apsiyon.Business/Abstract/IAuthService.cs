using Apsiyon.Core.Entities.Concrete;
using Apsiyon.Core.Utilities.Results;
using Apsiyon.Core.Utilities.Security.Jwt;
using Apsiyon.Entities.Dtos;

namespace Apsiyon.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
