using Apsiyon.Entities.Concrete;
using Apsiyon.Entities.Dtos;
using Apsiyon.Utilities.Results;
using Apsiyon.Utilities.Security.Jwt;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
        Task<IResult> UserExists(string email);
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
    }
}
