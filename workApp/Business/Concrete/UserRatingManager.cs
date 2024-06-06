using System.Linq.Expressions;
using Business.Abstract;
using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class UserRatingManager : IUserRatingService
{
    private readonly IUserRatingRepository _ratingRepository;
    private readonly IUserService _userService;

    public UserRatingManager
        (
        IUserRatingRepository ratingRepository,
        IUserService userService
        )
    {
        _userService = userService;
        _ratingRepository = ratingRepository;
    }

    public IDataResult<UserRating> Get(int id)
    {
        UserRating userRating = _ratingRepository.Get(r => r.Id == id);
        if (userRating == null)
        {
            return new ErrorDataResult<UserRating>("Kullanıcıya ait puan bilgileri getirilemedi");
        }
        return new SuccessDataResult<UserRating>(userRating, "Kullanıcıya ait puan bilgileri başarıyla getirildi");
    }

    public IResult Create(UserRating userRating)
    {
        UserRating createdUserRating = _ratingRepository.Create(userRating);
        if (this.Get(userRating.UserId).Data != null)
        {
            return new ErrorResult("Kullanıcıya ait puan bilgisi zaten var");
        }

        return new SuccessResult("Kullanıcıya ait puan bilgisi başarıyla oluşturuldu");
    }

    public IResult Delete(int id)
    {
        UserRating tempRating = this.Get(id).Data;
        if (tempRating != null)
        {
            _ratingRepository.Delete(tempRating);
            return new SuccessResult("Kullanıcıya ait puan bilgisi başarıyla silindi");   
        }
        return new ErrorResult("Kullanıcıya ait puan bilgisi silinemedi");
    }

    public IDataResult<UserRating> Update(UserRating userRating)
    {
        UserRating rating = _ratingRepository.Update(userRating);
        if (rating != null)
        {
            return new SuccessDataResult<UserRating>(rating, "Kullanıcıya ait puan bilgisi başarıyla güncellendi");
        }

        return new ErrorDataResult<UserRating>("Kullanıcıya ait puan bilgisi güncellenemedi");
    }
}