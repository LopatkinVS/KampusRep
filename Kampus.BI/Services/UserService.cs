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
        private readonly IContentService _contentService;

        public UserService(IUserRepository userRepository, 
            IReviewRepository reviewRepository,
            IContentService contentService)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _contentService = contentService;
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
            _contentService.CountProfessorRating(review.Professor.Id);
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
            _contentService.CountProfessorRating(review.Professor.Id);
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
            _contentService.CountProfessorRating(GetReview(reviewId).Professor.Id);
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public List<Review> GetUserReviews(int userId)
        {
            return _userRepository.GetSingle(userId).Reviews.ToList();
        }

        public Review GetReview(int reviewId)
        {
            return _reviewRepository.GetSingle(reviewId);
        }
    }
}
