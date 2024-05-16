using Business.Abstract;
using Business.Constants;
using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete.Dto.Requests.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private IUserOperationClaimService _userOperationClaimService;
        private ITokenHelper _tokenHelper;
        public AuthManager(
            IUserService userService,
            IUserOperationClaimService userOperationClaimService,
            ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _userOperationClaimService = userOperationClaimService;
            _userService = userService;

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Message.AccessTokenCreated);
        }

        public IDataResult<User> Login(LoginRequestDto req)
        {
            var userToCheck = _userService.GetByMail(req.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Message.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(req.Password, userToCheck.PasswordSalt, userToCheck.PasswordHash))
            {
                return new ErrorDataResult<User>(Message.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Message.SuccessfulLogin);
        }

        public IDataResult<User> Register(RegisterRequestDto req, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordSalt, out passwordHash);
            var user = new User
            {
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            var createdUser = _userService.Create(user).Data;
            UserOperationClaim c = new UserOperationClaim()
            {
                UserId = createdUser.Id,
                OperationClaimId = req.UserType,
            };
            _userOperationClaimService.Create(c);

            return new SuccessDataResult<User>(user, Message.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Message.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
