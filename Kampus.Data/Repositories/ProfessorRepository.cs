using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kampus.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly KampusContext _context;

        public ProfessorRepository(KampusContext context)
        {
            _context = context;
        }

        public bool CreateProfessor(Professor professor)
        {
            _context.Add(professor);
            return Save();
        }

        public ICollection<Review> GetAllProfessorReviews(int professorId)
        {
            return _context.Professors.Where(p => p.Id == professorId)
                .SelectMany(r => r.Reviews)
                .ToList();
        }

        public ICollection<Professor> GetProfessors()
        {
            return _context.Professors.ToList();
        }

        public ICollection<Professor> GetProfessorsByUniversity(int universityId)
        {
            return _context.Universities.Where(u => u.Id == universityId)
                .SelectMany(p => p.Professors)
                .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
