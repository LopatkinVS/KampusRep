using Kampus.Model.Entities;

namespace Kampus.Data.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        ICollection<Review> GetAllUserReviews(int id);
        User GetUserByLastName(string lastName);
        bool UserExists(int userID);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
