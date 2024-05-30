using System.Linq.Expressions;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfTaskCollectionDal : EfEntityRepositoryBase<TaskCollection, EfDbContext>, ITaskCollectionRepository
{
    private readonly EfDbContext _context;

    public EfTaskCollectionDal(EfDbContext context)
    {
        _context = context;
    }
}