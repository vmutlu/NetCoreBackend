using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Entities.Concrete;
using Apsiyon.Entities.Dtos;
using Apsiyon.Utilities.Results;
using Apsiyon.Utilities.Security.Hashing;
using Apsiyon.Utilities.Security.Jwt;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken =  _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByEmail(userForLoginDto.Email);

            if (userToCheck is null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                return new ErrorDataResult<User>(Messages.PasswordError);

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessFullLogin);
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            await _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public async Task<IResult> UserExists(string email)
        {
            if (await _userService.GetByEmail(email) is not null)
                return new ErrorResult(Messages.UserAlreadyExists);

            return new SuccessResult();
        }
    }
}
