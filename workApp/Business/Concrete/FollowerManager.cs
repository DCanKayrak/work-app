using System.Linq.Expressions;
using Business.Abstract;
using Business.BusinessAspects;
using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Enums;

namespace Business.Concrete;

public class FollowerManager : IFollowerService
{
    private readonly IFollowerRepository _followerRepository;
    private readonly IUserService _userService;

    public FollowerManager
    (
        IFollowerRepository followerRepository,
        IUserService userService
    )
    {
        _userService = userService;
        _followerRepository = followerRepository;
    }
    
    [SecuredOperation("USER")]
    public IDataResult<List<Follower>> GetAll(Expression<Func<Follower, bool>> filter)
    {
        int authUser = _userService.GetAuthUser().Id;
        List<Follower> followers = _followerRepository.GetAll(f => f.From == authUser && f.Status == FollowerStatus.Accepted);
        if ( followers != null )
        {
            return new SuccessDataResult<List<Follower>>(followers);
        }
        return new ErrorDataResult<List<Follower>>("Takipçiler bulunamadı");

    }
    [SecuredOperation("USER")]
    public IDataResult<Follower> Get(int id)
    {
        Follower follower = _followerRepository.Get(f => f.Id == id);
        if ( follower != null )
        {
            return new SuccessDataResult<Follower>(follower);
        }
        return new ErrorDataResult<Follower>("Seçtiğiniz id'ye ait takipçi bulunamadı");

    }
    [SecuredOperation("USER")]
    public IDataResult<Follower> Create(Follower entity)
    {
        int authUser = _userService.GetAuthUser().Id;
        entity.From = authUser;
        if (_followerRepository.Get(f => f.From == authUser && f.To == entity.To) != null)
        {
            return new ErrorDataResult<Follower>("Zaten isteğiniz gönderilmiş.");
        }
        
        Follower follower = _followerRepository.Create(entity);
        if ( follower != null )
        {
            return new SuccessDataResult<Follower>(follower);
        }
        return new ErrorDataResult<Follower>("İstek gönderirken hata");
    }
    
    [SecuredOperation("USER")]
    public IResult Update(Follower entity)
    {
        throw new NotImplementedException();
    }

    [SecuredOperation("USER")]
    public IResult Delete(int id)
    {
        Follower follower = _followerRepository.Get(f => f.Id == id);
        if ( follower != null )
        {
            _followerRepository.Delete(follower);
            return new SuccessResult();
        }
        return new ErrorResult();
    }
    
    [SecuredOperation("USER")]
    public IResult RespondRequest(int id, bool response)
    {
        Follower follower = _followerRepository.Get(f => f.Id == id);
        if ( follower != null )
        {
            follower.Status = response ? FollowerStatus.Accepted : FollowerStatus.Rejected;
            _followerRepository.Update(follower);
            return new SuccessDataResult<Follower>(follower);
        }
        return new ErrorDataResult<Follower>();

    }
}