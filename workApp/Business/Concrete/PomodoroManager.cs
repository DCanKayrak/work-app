using Business.Abstract;
using Business.DependencyResolvers.Mapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PomodoroManager : IPomodoroService
    {
        private IPomodoroRepository _pomodoroRepository;

        public PomodoroManager
            (
            IPomodoroRepository pomodoroRepository
            )
        {
            this._pomodoroRepository = pomodoroRepository;
        }

        public IDataResult<Pomodoro> Create(Pomodoro entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Pomodoro> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Pomodoro>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<PomodoroResponse>> GetAllWithUserAndDate(int userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Pomodoro entity)
        {
            throw new NotImplementedException();
        }
    }
}
