using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Core.Entities.Concrete;
using Apsiyon.Core.Utilities.Results;
using Apsiyon.Core.Utilities.Security.Hashing;
using Apsiyon.Core.Utilities.Security.Jwt;
using Apsiyon.Entities.Dtos;

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
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);

            if (userToCheck is null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                return new ErrorDataResult<User>(Messages.PasswordError);

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessFullLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
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

            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByEmail(email) is not null)
                return new ErrorResult(Messages.UserAlreadyExists);

            return new SuccessResult();
        }
    }
}
