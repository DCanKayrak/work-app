using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICrudService<TEntity> : IService
    {
        IDataResult<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter);
        IDataResult<TEntity> Get(int id);
        IDataResult<TEntity> Create(TEntity entity);
        IResult Update(TEntity entity);
        IResult Delete(int id);
    }
}
