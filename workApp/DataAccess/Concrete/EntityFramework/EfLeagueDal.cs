﻿using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfLeagueDal : EfEntityRepositoryBase<League, EfDbContext>, ILeagueRepository
{
    
}