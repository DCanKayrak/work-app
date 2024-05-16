using AutoMapper;
using Business.Abstract;
using Business.DependencyResolvers.Mapper;
using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dto.Responses.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;

        public UserManager
            (
            IUserRepository userRepository
            )
        {
            _userRepository = userRepository;
        }
        public IDataResult<User> Create(User entity)
        {
            _userRepository.Create(entity);
            return new SuccessDataResult<User>(entity);
        }

        public IResult Delete(int id)
        {
            _userRepository.Delete(_userRepository.Get(u => u.Id == id));
            return new SuccessResult();
        }

        public IDataResult<User> Get(int id)
        {
            return new SuccessDataResult<User>(_userRepository.Get(u => u.Id == id));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userRepository.GetAll(null));
        }
        public User GetByMail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userRepository.GetClaims(user);
        }

        public IResult Update(User entity)
        {
            _userRepository.Update(entity);
            return new SuccessResult();
        }

        public User GetAuthUser()
        {
            string email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            return this.GetByMail(email);
        }
    }
}
