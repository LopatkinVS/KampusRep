using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kampus.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly KampusContext _context;

        public ReviewRepository(KampusContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewByProfessor(int professorId)
        {
            return _context.Professors.Where(p => p.Id == professorId)
                .SelectMany(p => p.Reviews)
                .ToList();
        }

        public ICollection<Review> GetReviewByUser(int userId)
        {
            return _context.User.Where(u => u.Id == userId)
                .SelectMany(u => u.Reviews)
                .ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
