using Business.Abstract;
using Business.DependencyResolvers.Mapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests.Pomodoro;
using Entities.Concrete.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;

namespace Business.Concrete
{
    [SecuredOperation("USER")]
    public class PomodoroManager : IPomodoroService
    {
        private readonly IUserService _userService;
        private readonly IPomodoroRepository _pomodoroRepository;

        public PomodoroManager
            (
            IPomodoroRepository pomodoroRepository,
            IUserService userService
            )
        {
            _userService = userService;
            _pomodoroRepository = pomodoroRepository;
        }


        public IDataResult<Pomodoro> Create(Pomodoro entity)
        {
            entity.UserId = _userService.GetAuthUser().Id;
            return new SuccessDataResult<Pomodoro>(_pomodoroRepository.Create(entity));
        }

        public IResult Delete(int id)
        {
            Pomodoro p = _pomodoroRepository.Get(p => p.Id == id);
            if ( p != null )
            {
                _pomodoroRepository.Delete(p);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<Pomodoro> Get(int id)
        {
            Pomodoro p = _pomodoroRepository.Get(p => p.Id == id);
            if ( p != null )
            {
                return new SuccessDataResult<Pomodoro>(p);
            }


            return new ErrorDataResult<Pomodoro>(null,"Pomodoro bulunamadı");
        }

        public IDataResult<List<Pomodoro>> GetAll(Expression<Func<Pomodoro, bool>> filter)
        {
            List<Pomodoro> pomodoros = _pomodoroRepository.GetAll(filter);
            if ( pomodoros != null) {
                return new SuccessDataResult<List<Pomodoro>>(pomodoros);
            }
            return new ErrorDataResult<List<Pomodoro>>(null,"Pomodorolar getirilirken bir hatayla karşılaşıldı");
        }

        public IDataResult<List<Pomodoro>> GetAllWithUserAndDate(int userId, DateTime date)
        {
            List<Pomodoro> pomodoros = _pomodoroRepository.GetAll(p => p.Created_At.Date == date.Date && p.UserId == userId);
            if ( pomodoros != null )
            {
                return new SuccessDataResult<List<Pomodoro>>(pomodoros);
            }

            throw new CustomError(ErrorEnum.GET_ALL_POMODOROS_WITH_USER_AND_DATE);
        }

        public IDataResult<Double> GetTotalPomodoroDuration()
        {
            int authUser = _userService.GetAuthUser().Id;
            var pomodoros = _pomodoroRepository.GetAll(p => p.UserId == authUser);
            int totalDuration = pomodoros.Sum(p => p.Duration);

            return new SuccessDataResult<Double>(totalDuration);
        }
        
        public IDataResult<List<Pomodoro>> GetAllBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            List<Pomodoro> pomodoros = _pomodoroRepository.GetAll(
                p => p.Created_At.Date >= startDate.Date && 
                     p.Created_At.Date <= endDate.Date && 
                     p.UserId == _userService.GetAuthUser().Id
            ).ToList();

            if (pomodoros != null && pomodoros.Count > 0)
            {
                return new SuccessDataResult<List<Pomodoro>>(pomodoros);
            }
            return new ErrorDataResult<List<Pomodoro>>("Seçtiğiniz tarih aralığı veya kullanıcı id'sine ait pomodoro bulunamadı");
        }
        
        public IResult Update(Pomodoro entity)
        {
            throw new NotImplementedException();
        }
    }
}
