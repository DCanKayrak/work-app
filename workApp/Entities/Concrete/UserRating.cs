namespace Entities.Concrete;

public class UserRating : BaseEntity
{
    public int UserId { get; set; }
    public double Score { get; set; } = 0;
    public int LeagueId { get; set; } = 0;
    
    public UserRating(){}
    public UserRating(int userId)
    {
        UserId = userId;
    }
}