using Kampus.BI.Interfaces;
using Kampus.Data.Abstract;
using Kampus.Model.Entities;
using System.Runtime.CompilerServices;

namespace Kampus.BI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserService(IUserRepository userRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
        }

        public void CreateUser(User user)
        {
            _userRepository.Create(user);
            _userRepository.Save();
        }

        public void CreateReview(Review review)
        {
            _reviewRepository.Create(review);
            _reviewRepository.Save();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.Update(review);
            _reviewRepository.Save();
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Delete(GetUser(userId));
            _userRepository.Save();
        }

        public void DeleteReview(int reviewId)
        {
            _reviewRepository.Delete(GetReview(reviewId));
            _reviewRepository.Save();
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public Review GetReview(int reviewId)
        {
            return _reviewRepository.GetSingle(reviewId);
        }
    }
}
