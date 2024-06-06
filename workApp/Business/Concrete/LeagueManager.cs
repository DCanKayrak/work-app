using System.Linq.Expressions;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class LeagueManager : ILeagueService
{
    private readonly ILeagueRepository _leagueRepository;

    public LeagueManager
        (
            ILeagueRepository leagueRepository    
        )
    {
        _leagueRepository = leagueRepository;
    }
    public IDataResult<List<League>> GetAll(Expression<Func<League, bool>> filter)
    {
        List<League> leagues = _leagueRepository.GetAll(null);
        if (leagues == null)
        {
            return new ErrorDataResult<List<League>>("Ligler getirilemedi");
        }

        return new SuccessDataResult<List<League>>(leagues);
    }

    public IDataResult<League> Get(int id)
    {
        League league = _leagueRepository.Get(l => l.Id == id);
        if (league == null)
        {
            return new ErrorDataResult<League>("Lig getirilemedi");
        }

        return new SuccessDataResult<League>(league);
    }

    public IDataResult<League> Create(League entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(League entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(int id)
    {
        throw new NotImplementedException();
    }
}