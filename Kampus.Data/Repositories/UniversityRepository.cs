using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using System.Net.Http.Headers;

namespace Kampus.Data.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly KampusContext _context;

        public UniversityRepository(KampusContext context)
        {
            _context = context;
        }

        public bool CreateUniversity(University university)
        {
            _context.Add(university);
            return Save();
        }

        public ICollection<Professor> GetAllProffesorsFormUniversity(int id)
        {
            return _context.Universities.Where(u => u.Id == id)
                .SelectMany(u => u.Professors)
                .ToList();
        }

        public ICollection<University> GetUniversities()
        {
            return _context.Universities.ToList();
        }

        public University GetUniversityById(int id)
        {
            return _context.Universities.Where(u => u.Id == id)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
