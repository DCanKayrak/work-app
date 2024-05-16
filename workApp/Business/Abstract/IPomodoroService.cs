﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPomodoroService : ICrudService<Pomodoro>
    {
        public IDataResult<List<PomodoroResponse>> GetAllWithUserAndDate(int userId, DateTime date);
    }
}