using Kampus.Model.Entities;

namespace Kampus.BI.Interfaces
{
    public interface IUserService
    {
        void CreateUser(User user);
        void CreateReview(Review review);
        void UpdateUser(User user);
        void UpdateReview(Review review);
        void DeleteUser(int userId);
        void DeleteReview(int reviewId);
        User GetUser(int userId);
        List<User> GetUsers();
        Review GetReview(int reviewId);
    }
}
