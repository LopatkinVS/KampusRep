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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
