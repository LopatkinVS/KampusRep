using Kampus.Data.Interfaces;
using Kampus.Model.Entities;

namespace Kampus.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KampusContext _context;

        public UserRepository(KampusContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public ICollection<Review> GetAllUserReviews(int id)
        {
            return _context.User.Where(u => u.Id == id)
                .SelectMany(u => u.Reviews)
                .ToList();
        }

        public User GetUserById(int id)
        {
            return _context.User.Where(u => u.Id == id)
                .FirstOrDefault();

        }
        public User GetUserByLastName(string lastName)
        {
            return _context.User.Where(u => u.LastName == lastName)
                .FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.User.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userID)
        {
            return _context.User.Any(u => u.Id == userID);
        }
    }
}
