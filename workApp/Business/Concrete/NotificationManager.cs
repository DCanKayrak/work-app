using System.Linq.Expressions;
using Business.Abstract;
using Business.BusinessAspects;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

[SecuredOperation("USER")]
public class NotificationManager : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserService _userService;

    public NotificationManager
        (
            INotificationRepository notificationRepository,
            IUserService userService
        )
    {
        _userService = userService;
        _notificationRepository = notificationRepository;
    }

    public IDataResult<List<Notification>> GetAll(Expression<Func<Notification, bool>> filter)
    {
        int authUser = _userService.GetAuthUser().Id;
        List<Notification> list = _notificationRepository.GetAll(n => n.UserId == authUser);
        if (list == null)
        {
            return new ErrorDataResult<List<Notification>>("Seçilen kullanıcıya ait bildirimler getirilemedi");
        }

        return new SuccessDataResult<List<Notification>>(list, "Bildirimler başarıyla getirildi");
    }

    public IDataResult<Notification> Get(int id)
    {
        Notification notification = _notificationRepository.Get(n => n.Id == id);
        if (notification == null)
        {
            return new ErrorDataResult<Notification>("Verilen id değerine sahip bildirim getirilemedi");
        }

        return new SuccessDataResult<Notification>(notification, "Bildirim başarıyla getirildi");
    }
    
    public IDataResult<Notification> Create(Notification entity)
    {
        int authUser = _userService.GetAuthUser().Id;
        entity.UserId = authUser;
        Notification notification = _notificationRepository.Create(entity);
        if (notification == null)
        {
            return new ErrorDataResult<Notification>("Bildirim oluşturulamadı");
        }

        return new SuccessDataResult<Notification>(notification, "Bildirim başarıyla oluşturuldu");
    }

    public IResult Update(Notification entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(int id)
    {
        int authUser = _userService.GetAuthUser().Id;
        Notification notification = this.Get(id).Data;
        if (notification == null || notification.UserId != authUser)
        {
            return new ErrorDataResult<Notification>("Bildirim silinemedi");
        }

        return new SuccessDataResult<Notification>(notification, "Bildirim başarıyla silindi");
    }
}