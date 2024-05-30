using System.Linq.Expressions;
using Business.Abstract;
using Business.BusinessAspects;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class TaskCollectionManager : ITaskCollectionService
{
    private readonly ITaskCollectionRepository _taskCollectionRepository;
    private readonly IUserService _userService;
    private readonly ITaskService _taskService;

    public TaskCollectionManager(ITaskService taskService, ITaskCollectionRepository taskCollectionRepository, IUserService userService)
    {
        _taskCollectionRepository = taskCollectionRepository;
        _userService = userService;
        _taskService = taskService;
    }
    
    [SecuredOperation("USER")]
    public IDataResult<List<TaskCollection>> GetAll(Expression<Func<TaskCollection, bool>> filter)
    {
        int userId = _userService.GetAuthUser().Id;
        List<TaskCollection> collections = _taskCollectionRepository.GetAll(c => c.UserId == userId && (filter == null || filter.Compile().Invoke(c)));
        
        if (collections == null || !collections.Any())
        {
            return new ErrorDataResult<List<TaskCollection>>("Koleksiyonlar getirilemedi");
        }
        
        foreach (var item in collections)
        {
            item.Tasks = _taskService.GetAll(t => t.CollectionId == item.Id).Data;
        }

        return new SuccessDataResult<List<TaskCollection>>(collections, "Koleksiyonlar başarıyla getirildi");
    }


    [SecuredOperation("USER")]
    public IDataResult<TaskCollection> Get(int id)
    {
        int userId = _userService.GetAuthUser().Id;
        TaskCollection tempCollection = _taskCollectionRepository.Get(t => t.Id == id);
        if (tempCollection != null)
        {
            tempCollection.Tasks = _taskService.GetAll(t => t.CollectionId == id).Data;
            return new SuccessDataResult<TaskCollection>(tempCollection);
        }

        return new ErrorDataResult<TaskCollection>("Hata!");
    }
    
    [SecuredOperation("USER")]
    public IDataResult<TaskCollection> Create(TaskCollection entity)
    {
        int userId = _userService.GetAuthUser().Id;
        entity.UserId = userId;
        TaskCollection tempCollection = _taskCollectionRepository.Create(entity);
        if (tempCollection != null)
        {
            return new SuccessDataResult<TaskCollection>(entity);
        }

        return new ErrorDataResult<TaskCollection>("Hata!");
    }
    [SecuredOperation("USER")]
    public IResult Update(TaskCollection entity)
    {
        throw new NotImplementedException();
    }

    [SecuredOperation("USER")]
    public IResult Delete(int id)
    {
        int userId = _userService.GetAuthUser().Id;
        TaskCollection collection = _taskCollectionRepository.Get(c => c.Id == id);

        if (collection.UserId != userId)
        {
            return new ErrorResult("Bu koleksiyon size ait değildir.");
        }

        if (collection != null)
        {
            _taskCollectionRepository.Delete(collection);
            return new SuccessResult("Koleksiyon başarıyla silindi");
        }

        return new ErrorResult("Silme sırasında bir hata meydana geldi");
    }
}