using Kampus.Model.Entities;

namespace Kampus.Data.Interfaces
{
    public interface IProfessorRepository
    {
        ICollection<Professor> GetProfessors();
        ICollection<Professor> GetProfessorsByUniversity(int universityId);
        ICollection<Review> GetAllProfessorReviews(int professorId);
        bool CreateProfessor(Professor professor);
        bool Save();
    }
}
