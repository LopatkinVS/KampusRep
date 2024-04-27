using Kampus.Model.Entities;

namespace Kampus.Data.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        ICollection<Review> GetReviewByUser(int userId);
        ICollection<Review> GetReviewByProfessor(int professorId);
        bool CreateReview(Review review);
        bool Save();
    }
}
