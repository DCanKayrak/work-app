using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfPomodoroDal : IPomodoroRepository
    {
        public void Create(Pomodoro entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Pomodoro entity)
        {
            throw new NotImplementedException();
        }

        public Pomodoro Get(Expression<Func<Pomodoro, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Pomodoro> GetAll(Expression<Func<Pomodoro, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Pomodoro entity)
        {
            throw new NotImplementedException();
        }
    }
}
