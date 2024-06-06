using System.Linq.Expressions;
using Business.Abstract;
using Business.BusinessAspects;
using Business.DependencyResolvers.Mapper;
using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests.Notification;
using Entities.Concrete.Enums;

namespace Business.Concrete;

public class FollowerManager : IFollowerService
{
    private readonly IFollowerRepository _followerRepository;
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;

    public FollowerManager
    (
        IFollowerRepository followerRepository,
        INotificationService notificationService,
        IUserService userService
    )
    {
        _userService = userService;
        _notificationService = notificationService;
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

        if (_userService.Get(entity.To) == null)
        {
            return new ErrorDataResult<Follower>("Takip isteğini gönderdiğiniz kişi bulunamadı");
        }
        
        Follower follower = _followerRepository.Create(entity);
        if ( follower != null )
        {
            _notificationService.Create(MapperHelper<CreateNotificationRequest,Notification>.Map(new CreateNotificationRequest(entity.To,"Yeni bağlantı isteğiniz bulunmakta", NotificationType.FOLLOWER_NOTIFICATION)));
            return new SuccessDataResult<Follower>(follower);
        }
        return new ErrorDataResult<Follower>("Takip İsteği gönderilemedi");
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

    public IDataResult<List<Follower>> GetFollowerRequests()
    {
        int authUser = _userService.GetAuthUser().Id;
        List<Follower> requests = _followerRepository.GetAll(f => f.To == authUser && f.Status == FollowerStatus.Pending);
        if (requests != null)
        {
            return new SuccessDataResult<List<Follower>>(requests);
        }

        return new ErrorDataResult<List<Follower>>("Bekleyen takip istekleri getirilemedi");
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